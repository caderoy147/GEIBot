#include <Stepper.h>
#include <Servo.h>

// Data declarations
int zAxisValue;
int yAxisValue;
int hAxisValue;
char dl;
String data;
String h;

Servo myservo;  // create servo object to control a servo
Servo myservo1;  // create servo object to control a servo


int pos = 0;    // variable to store the servo position
int pos1 = 0;    // variable to store the servo position
const int stepsPerRotation = 1024;

int zSpeed = 1000;
int ySpeed = 1000;
int hSpeed = 3000;
int vSpeed = 15;
int wSpeed = 15;
// Define the pins

// Steps for full rotation
const int Z_STEPS_PER_REVOLUTION = 7800;  // 360 degrees
const int Y_STEPS_PER_REVOLUTION = 21240; // 360 * 30
const int H_STEPS_PER_REVOLUTION = 17640; // 230 degrees


// Stepper
#define EN_PIN    8 //enable 

// Z axis (bottom)
#define Z_STEP_PIN  4 //step 4
#define Z_DIR_PIN  7 //direction 7

// Y axis
#define Y_STEP_PIN  3 //step //3  53
#define Y_DIR_PIN  6 //direction //6 if module  57

// H axis
#define H_STEP_PIN  2 //step 2
#define H_DIR_PIN  5 //direction  5

// ULN stepper (for gripper)
#define OUTPUT1   31  // Connected to the Blue coloured wire 7
#define OUTPUT2   35  // Connected to the Pink coloured wire 5
#define OUTPUT3   33  // Connected to the Yellow coloured wire 6
#define OUTPUT4   37  // Connected to the Orange coloured wire 4

//41 TEMP

// Servo motor pins
#define SERVO_PIN_1  45  // V axis
#define SERVO_PIN_2  46  // W axis

Stepper myStepper(stepsPerRotation, OUTPUT1, OUTPUT2, OUTPUT3, OUTPUT4);

void setup() 
{
  // Servo
  myservo.attach(SERVO_PIN_1);  // V axis
  myservo1.attach(SERVO_PIN_2);  // W axis

  // Set pin modes
  pinMode(EN_PIN, OUTPUT);
  digitalWrite(EN_PIN, HIGH); // deactivate driver (LOW active)

  // Z motor config (bottom)
  pinMode(Z_DIR_PIN, OUTPUT);
  digitalWrite(Z_DIR_PIN, LOW);
  pinMode(Z_STEP_PIN, OUTPUT);
  digitalWrite(Z_STEP_PIN, LOW);

  // Y motor config
  pinMode(Y_DIR_PIN, OUTPUT);
  pinMode(Y_STEP_PIN, OUTPUT);

  // H motor config
  pinMode(H_DIR_PIN, OUTPUT);
  digitalWrite(H_DIR_PIN, LOW);
  pinMode(H_STEP_PIN, OUTPUT);
  digitalWrite(H_STEP_PIN, LOW);

  digitalWrite(EN_PIN, LOW); // activate driver

  // Stepper setup speed
  myStepper.setSpeed(15); // 15 RPM
  
  Serial.begin(9600);
}

void loop() {
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
        case 'G':
          grip();
          disableGripper();
          break;
        case 'R':
          unGrip();
          disableGripper();
          break;
      }
    }
  }
}


void ZrotateMotor(int steps, bool direction, int speed) {
  digitalWrite(Z_DIR_PIN, direction);
  for (int i = 0; i < steps; i++) {
    digitalWrite(Z_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(Z_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }
}

void YrotateMotor(int steps, bool direction, int speed) {
  digitalWrite(Y_DIR_PIN, direction);
  for (int i = 0; i < steps; i++) {
    digitalWrite(Y_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(Y_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }
}

void HrotateMotor(int steps, bool direction, int speed) {
  digitalWrite(H_DIR_PIN, direction);
  for (int i = 0; i < steps; i++) {
    digitalWrite(H_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(H_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }
}

void setServoPosition(Servo servo, int newPos, int speed) {
  newPos = constrain(newPos, 0, 180);  // Ensure newPos is within 0-180 range
  int currentPos = servo.read();
  
  if (currentPos < newPos) {
    for (int i = currentPos; i <= newPos; i++) {
      servo.write(i);
      delay(speed);
    }
  } else {
    for (int i = currentPos; i >= newPos; i--) {
      servo.write(i);
      delay(speed);
    }
  }
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

void processMovement(char axis, int value) {
  int steps;
  bool direction;
  
  switch(axis) {
    case 'Z':
      steps = map(value, 0, 360, 0, Z_STEPS_PER_REVOLUTION);
      direction = (steps >= 0);
      ZrotateMotor(abs(steps), direction, zSpeed);
      break;
    case 'Y':
      steps = map(value, 0, 180, 0, Y_STEPS_PER_REVOLUTION / 2);
      direction = (steps >= 0);
      YrotateMotor(abs(steps), direction, ySpeed);
      break;
    case 'H':
      steps = map(value, 0, 230, 0, H_STEPS_PER_REVOLUTION);
      direction = (steps >= 0);
      HrotateMotor(abs(steps), direction, hSpeed);
      break;
    case 'V':
      setServoPosition(myservo, value, vSpeed);
      break;
    case 'W':
      setServoPosition(myservo1, value, wSpeed);
      break;
  }
}
void updateSpeed(String speedData) {
  char axis = speedData.charAt(0);
  int speed = speedData.substring(1).toInt();
  
  switch(axis) {
    case 'Z':
      zSpeed = map(speed, 0, 100, 2000, 100); // Map 0-100 to 2000-100 (slower to faster)
      break;
    case 'Y':
      ySpeed = map(speed, 0, 100, 2000, 100);
      break;
    case 'H':
      hSpeed = map(speed, 0, 100, 4000, 100);
      break;
    case 'V':
      vSpeed = map(speed, 0, 100, 30, 1);
      break;
    case 'W':
      wSpeed = map(speed, 0, 100, 30, 1);
      break;
  }
}