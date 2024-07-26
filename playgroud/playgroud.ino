#include <AccelStepper.h>
#include <MultiStepper.h>
#include <Servo.h>

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

// Create the stepper motor objects
AccelStepper stepperZ(AccelStepper::DRIVER, Z_STEP_PIN, Z_DIR_PIN);
AccelStepper stepperY(AccelStepper::DRIVER, Y_STEP_PIN, Y_DIR_PIN);
AccelStepper stepperH(AccelStepper::DRIVER, H_STEP_PIN, H_DIR_PIN);

// Create the MultiStepper object
MultiStepper steppers;

Servo myservo;  // V axis
Servo myservo1; // W axis

// Array to store target positions
long positions[3];

void setup() 
{
  // Initialize the enable pin
  pinMode(EN_PIN, OUTPUT);
  digitalWrite(EN_PIN, LOW); // Activate driver (LOW active)

  // Set maximum speed for each stepper
  stepperZ.setMaxSpeed(1000);
  stepperY.setMaxSpeed(1000);
  stepperH.setMaxSpeed(1000);

  // Add steppers to the MultiStepper object
  steppers.addStepper(stepperZ);
  steppers.addStepper(stepperY);
  steppers.addStepper(stepperH);

  // Servo setup
  myservo.attach(SERVO_PIN_1);
  myservo1.attach(SERVO_PIN_2);
  
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
        case 'S':
          updateSpeed(value);
          break;
        case 'C':
          crabDance();
          break;
      }
    }
  }
}

void processMovement(char axis, int value) {
  // Read current positions
  positions[0] = stepperZ.currentPosition();
  positions[1] = stepperY.currentPosition();
  positions[2] = stepperH.currentPosition();

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
      myservo.write(value);
      Serial.println("MC"); // Movement Complete
      return;
    case 'W':
      myservo1.write(value);
      Serial.println("MC"); // Movement Complete
      return;
  }
  
  // Set the target positions and run the steppers
  steppers.moveTo(positions);
  steppers.runSpeedToPosition();
  Serial.println("MC"); // Movement Complete
}

void updateSpeed(String speedData) {
  char axis = speedData.charAt(0);
  int speed = speedData.substring(1).toInt();
  
  switch(axis) {
    case 'Z':
      stepperZ.setMaxSpeed(map(speed, 0, 100, 100, 2000));
      Serial.print("Updating Z speed to "); Serial.println(stepperZ.maxSpeed());
      break;
    case 'Y':
      stepperY.setMaxSpeed(map(speed, 0, 100, 100, 2000));
      Serial.print("Updating Y speed to "); Serial.println(stepperY.maxSpeed());
      break;
    case 'H':
      stepperH.setMaxSpeed(map(speed, 0, 100, 100, 2000));
      Serial.print("Updating H speed to "); Serial.println(stepperH.maxSpeed());
      break;
  }
}

void crabDance() {
   long positions[3] = {1000, 1000, 1000}; // Move each stepper to 1000 steps

  // Move to the target positions
  steppers.moveTo(positions);
  steppers.runSpeedToPosition(); // Blocks until all steppers reach the position

  delay(1000); // Wait for a second

  // Move to different positions
  positions[0] = -1000;
  positions[1] = -1000;
  positions[2] = -1000;

  // Move to the new target positions
  steppers.moveTo(positions);
  steppers.runSpeedToPosition(); // Blocks until all steppers reach the position

  delay(1000); // Wait for a second
  Serial.println("DC"); // Dance Complete
}
