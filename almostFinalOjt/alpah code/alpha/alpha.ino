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

// Define the pins

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
    String data = Serial.readStringUntil('\n'); // Read data until newline character
    if (data.length() > 0) {
      char dl = data.charAt(0);
      String h = data.substring(1); // Trim any extra whitespace
      
      int steps = h.toInt();
      bool direction = (steps >= 0); // Determine direction based on the sign of steps
      steps = abs(steps); // Ensure steps are positive for the Stepper library

      switch(dl) {
        case 'Z':
          ZrotateMotor(steps, direction, 1000); // Rotate Z axis (bottom)
          break;
        case 'Y':
          YrotateMotor(steps, direction, 1000); // Rotate Y axis
          break;
        case 'H':
          HrotateMotor(steps, direction, 3000); // Rotate H axis
          break;
        case 'V':
          setServoPosition(myservo, steps, 15); // V axis (Servo 1)
          break;
        case 'W':
          setServoPosition(myservo1, steps, 15); // W axis (Servo 2)
          break;
        case 'G':
          grip(); // Control gripper
          disableGripper();
          break;
        case 'R':
          unGrip(); // Release gripper
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