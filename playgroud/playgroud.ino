#include <DHT.h>
#include <AccelStepper.h>
#include <MultiStepper.h>
#include <Servo.h>
#include <Stepper.h>

//temp
#define DHTPIN 41
// Define the stepper pins
#define EN_PIN    8 // Enable
// Z axis (bottom)
#define Z_STEP_PIN  4 
#define Z_DIR_PIN   7 
// Y axis
#define Y_STEP_PIN  3 
#define Y_DIR_PIN   6 
// H axis
#define H_STEP_PIN  2 
#define H_DIR_PIN   5 
// Servo motor pins
#define SERVO_PIN_1  45  // V axis
#define SERVO_PIN_2  46  // W axis
// ULN stepper (for gripper)
#define OUTPUT1   31  // Connected to the Blue coloured wire 7
#define OUTPUT2   35  // Connected to the Pink coloured wire 5
#define OUTPUT3   33  // Connected to the Yellow coloured wire 6
#define OUTPUT4   37  // Connected to the Orange coloured wire 4
// Create the stepper motor objects
AccelStepper stepperZ(AccelStepper::DRIVER, Z_STEP_PIN, Z_DIR_PIN);
AccelStepper stepperY(AccelStepper::DRIVER, Y_STEP_PIN, Y_DIR_PIN);
AccelStepper stepperH(AccelStepper::DRIVER, H_STEP_PIN, H_DIR_PIN);

// Create the MultiStepper object
MultiStepper steppers;
//temp obj
DHT dht;
Servo myservo;  // V axis
Servo myservo1; // W axis

const int stepsPerRotation = 1024;

Stepper myStepper(stepsPerRotation, OUTPUT1, OUTPUT2, OUTPUT3, OUTPUT4);
// Array to store target positions
long positions[3];

// Global variables to store current speeds
int speedZ = 1000;
int speedY = 1000;
int speedH = 1000;
//servo speeds
int speedV = 10;  // Default speed for V servo (degrees per second)
int speedW = 10;  // Default speed for W servo (degrees per second)
// Current positions for servos (DEFUALT)
int currentPosV = 90;
int currentPosW = 90;


void setup() 
{
  // Initialize the enable pin
  pinMode(EN_PIN, OUTPUT);
  digitalWrite(EN_PIN, LOW); // Activate driver (LOW active)

  // Set maximum speed for each stepper
  stepperZ.setMaxSpeed(speedZ);
  stepperY.setMaxSpeed(speedY);
  stepperH.setMaxSpeed(speedH);

  // Add steppers to the MultiStepper object
  steppers.addStepper(stepperZ);
  steppers.addStepper(stepperY);
  steppers.addStepper(stepperH);

  // Servo setup
  myservo.attach(SERVO_PIN_1);
  myservo1.attach(SERVO_PIN_2);

  dht.setup(41); 	/* set pin for data communication */

  myStepper.setSpeed(15); // 15 RPM
  
  Serial.begin(9600);
}

void loop() 
{
  if (Serial.available()) {
    String data = Serial.readStringUntil('\n');
    if (data.length() > 0) {
      char command = data.charAt(0);
      String value = data.substring(1);
      
      switch(command) {
        case 'Z':
        case 'Y':
        case 'H':
        case 'V':
        case 'W':
          processMovement(command, value.toInt());
          break;
       case 'G':
          grip();
          disableGripper();
          break;
        case 'R':
          unGrip();
          disableGripper();
          break;
        case 'S':
          updateSpeed(value);
          break;
        case 'T':
          sendSensorData();
          break;
        case 'C':
          crabDance();
          break;
      }
    }
  }
}

void updateSpeed(String speedData) {
  char axis = speedData.charAt(0);
  int speed = speedData.substring(1).toInt();
  
  switch(axis) {
    case 'Z':
      speedZ = map(speed, 0, 100, 100, 2000);
      stepperZ.setMaxSpeed(speedZ);
      Serial.print("Updated Z speed to "); Serial.println(speedZ);
      break;
    case 'Y':
      speedY = map(speed, 0, 100, 100, 2000);
      stepperY.setMaxSpeed(speedY);
      Serial.print("Updated Y speed to "); Serial.println(speedY);
      break;
    case 'H':
      speedH = map(speed, 0, 100, 100, 2000);
      stepperH.setMaxSpeed(speedH);
      Serial.print("Updated H speed to "); Serial.println(speedH);
      break;
    case 'V':
      speedV = map(speed, 0, 100, 1, 200);  // Map 0-100 to 10-180 degrees per second
      Serial.print("Updated V speed to "); Serial.println(speedV);
      break;
    case 'W':
      speedW = map(speed, 0, 100, 1, 200);  // Map 0-100 to 10-180 degrees per second
      Serial.print("Updated W speed to "); Serial.println(speedW);
      break;
  }
}
void processMovement(char axis, int value) {
  // Read current positions
  positions[0] = stepperZ.currentPosition();
  positions[1] = stepperY.currentPosition();
  positions[2] = stepperH.currentPosition();


    // Set the speeds before moving
  stepperZ.setMaxSpeed(speedZ);
  stepperY.setMaxSpeed(speedY);
  stepperH.setMaxSpeed(speedH);

  switch(axis) {
    case 'Z':
      positions[0] = map(value, 0, 360, 0, 7800);
      Serial.print("Moving Z to "); Serial.println(positions[0]);
      break;
    case 'Y':
      positions[1] = map(value, 0, 180, 0, 10800);
      Serial.print("Moving Y to "); Serial.println(positions[1]);
      break;
    case 'H':
      positions[2] = map(value, 0, 230, 0, 15480);
      Serial.print("Moving H to "); Serial.println(positions[2]);
      break;
    case 'V':
      moveServoWithSpeed(myservo, SERVO_PIN_1, value, speedV, currentPosV);
      currentPosV = value;
      break;
    case 'W':
      moveServoWithSpeed(myservo1, SERVO_PIN_2, value, speedW, currentPosW);
      currentPosW = value;
      break;
  }

  // Set the target positions and run the steppers
  steppers.moveTo(positions);
  steppers.runSpeedToPosition();
  Serial.println("MC"); // Movement Complete
}

void crabDance() {
   long positions[3] = {1000, 1000, 1000}; // Move each stepper to 1000 steps
  myservo.attach(SERVO_PIN_1); // Attach the servo
  myservo1.attach(SERVO_PIN_2); // Attach the servo
  // Move to the target positions
  steppers.moveTo(positions);
  steppers.runSpeedToPosition(); // Blocks until all steppers reach the position

  delay(1000); // Wait for a second
  myservo1.write(20);
  myservo.write(20);
  grip();


  // Move to different positions
  positions[0] = -1000;
  positions[1] = -1000;
  positions[2] = -1000;

  // Move to the new target positions
  steppers.moveTo(positions);
  steppers.runSpeedToPosition(); // Blocks until all steppers reach the position

  delay(1000); // Wait for a second
  myservo1.write(170);
  myservo.write(170);

  unGrip();
  myservo.detach(); // Detach the servo
  myservo1.detach(); // Detach the servo
  disableGripper();
  delay(1000);

  Serial.println("DC"); // Dance Complete
}
void grip() {
  myStepper.step(stepsPerRotation);  
  delay(1000);  // Delay between rotations
}

void unGrip() {
  myStepper.step(-stepsPerRotation);  
  delay(1000);  // Delay between rotations
}

void disableGripper() {
  digitalWrite(OUTPUT1, LOW);
  digitalWrite(OUTPUT2, LOW);
  digitalWrite(OUTPUT3, LOW);
  digitalWrite(OUTPUT4, LOW);
}

void moveServoWithSpeed(Servo &servo, int pin, int targetPos, int speed, int &currentPos) {
  servo.attach(pin);
  int diff = abs(targetPos - currentPos);
  int delayTime = 1000 / speed;  // Calculate delay time based on speed
  
  if (targetPos > currentPos) {
    for (int pos = currentPos; pos <= targetPos; pos++) {
      servo.write(pos);
      delay(delayTime);
    }
  } else {
    for (int pos = currentPos; pos >= targetPos; pos--) {
      servo.write(pos);
      delay(delayTime);
    }
  }
  
  servo.detach();
  currentPos = targetPos;
  Serial.println("MC"); // Movement Complete
}


void sendSensorData() {
  delay(dht.getMinimumSamplingPeriod());

  float humidity = dht.getHumidity();
  float temperature = dht.getTemperature();

  if (dht.getStatusString() == "OK") {
    Serial.print(humidity, 1);
    Serial.print(",");
    Serial.print(temperature, 1);
    Serial.print(",");
    Serial.println(dht.toFahrenheit(temperature), 1);
  } else {
    Serial.println("DHT sensor error");
  }
}