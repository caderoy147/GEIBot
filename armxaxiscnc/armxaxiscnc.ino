//libraries
#include <Stepper.h>
#include <Servo.h>

// data declarations
int xAxisValue;
int yAxisValue;
int zAxisValue;
char dl;
String data;
String h;




Servo myservo;  // create servo object to control a servo
Servo myservo1;  // create servo object to control a servo
// twelve servo objects can be created on most boards

int pos = 0;    // variable to store the servo position
int pos1 = 0;    // variable to store the servo position
const int stepsPerRotation = 1024;

// define the pins
// edited version-testing for cnc shield

//stepper
#define EN_PIN    8 //enable 

//X axis
#define X_STEP_PIN  2 //step
#define X_DIR_PIN  5 //direction

//y axis from otor driver tb2206 external
#define Y_STEP_PIN  53 //step
#define Y_DIR_PIN  51 //direction

//z axis
#define Z_STEP_PIN  4 //step
#define Z_DIR_PIN  7 //direction

//uln stepper
#define OUTPUT1   31                // Connected to the Blue coloured wire 7
#define OUTPUT2   35               // Connected to the Pink coloured wire 5
#define OUTPUT3   33                // Connected to the Yellow coloured wire 6
#define OUTPUT4   37                // Connected to the Orange coloured wire 4

// Servo motor pins
#define SERVO_PIN_1  45
#define SERVO_PIN_2  46


Stepper myStepper(stepsPerRotation, OUTPUT1, OUTPUT2, OUTPUT3, OUTPUT4);  
//Stepper myStepper = Stepper(stepsPerRevolution, 31, 35, 33, 37);
void setup() 
{

  //servo
  myservo.attach(SERVO_PIN_1);
  myservo1.attach(SERVO_PIN_2);  // attaches the servo on pin 9 to the servo object
  //set pin modes
  pinMode(EN_PIN, OUTPUT); // set the EN_PIN as an output
  digitalWrite(EN_PIN, HIGH); // deactivate driver (LOW active)

  //X  motor config
  pinMode(X_DIR_PIN, OUTPUT); // set the DIR_PIN as an output
  digitalWrite(X_DIR_PIN,LOW); // set the direction pin to low
  pinMode(X_STEP_PIN, OUTPUT); // set the STEP_PIN as an output
  digitalWrite(X_STEP_PIN, LOW); // set the step pin to low

  //Y  motor config
  pinMode(Y_DIR_PIN, OUTPUT); // set the DIR_PIN as an output
 // digitalWrite(Y_DIR_PIN,LOW); // set the direction pin to low
  pinMode(Y_STEP_PIN, OUTPUT); // set the STEP_PIN as an output
 // digitalWrite(Y_STEP_PIN, LOW); // set the step pin to low

  //Z  motor config
  pinMode(Z_DIR_PIN, OUTPUT); // set the DIR_PIN as an output
  digitalWrite(Z_DIR_PIN,LOW); // set the direction pin to low
  pinMode(Z_STEP_PIN, OUTPUT); // set the STEP_PIN as an output
  digitalWrite(Z_STEP_PIN, LOW); // set the step pin to low

  digitalWrite(EN_PIN, LOW); // activate driver

  //stepper setup sped
  myStepper.setSpeed(15);                      ;  // 15 RPM
 // myservo.setSpeed(15);
  //myservo1.setSpeed(15);  // attaches the servo on pin 9 to the servo object
  
  Serial.begin(9600);
}
void loop() {
  if (Serial.available()) {
    String data = Serial.readStringUntil('\n'); // Read data until newline character
    if (data.length() > 0) {
      char dl = data.charAt(0);
      String h = data.substring(1); // Trim any extra whitespace
      
      int steps = h.toInt();
      //int degrees = h.toInt();
      bool direction = (steps >= 0); // Determine direction based on the sign of steps
      steps = abs(steps); // Ensure steps are positive for the Stepper library

      switch(dl) {
        case 'X':
          XrotateMotor(steps, direction, 5000); // Rotate X axis based on direction and steps
          break;
        case 'Y':
          YrotateMotor(steps, direction, 10000); // Rotate Y axis based on direction and steps
          break;
        case 'Z':
          ZrotateMotor(steps, direction, 8000); // Rotate Z axis based on direction and steps
          break;
        case 'V':
          setServoPosition(myservo, steps);
          break;
        case 'W':
          setServoPosition(myservo1, steps);
          break;
        case 'G':
          grip();     // Control gripper for 'G' command
          break;
        case 'R':
          unGrip();   // Control ungripper for 'R' command
          break;
      }
    }
  }
}



void XrotateMotor(int steps, bool direction, int speed) {
  // Set the motor direction
  digitalWrite(X_DIR_PIN, direction);
  
  // Step the motor
  for (int i = 0; i < steps; i++) {
    digitalWrite(X_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(X_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }


}

void YrotateMotor(int steps, bool direction, int speed) {
  // Set the motor direction
  digitalWrite(Y_DIR_PIN, direction);
  
  // Step the motor
  for (int i = 0; i < steps; i++) {
    digitalWrite(Y_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(Y_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }
}


void ZrotateMotor(int steps, bool direction, int speed) {
  // Set the motor direction
  digitalWrite(Z_DIR_PIN, direction);
  
  // Step the motor
  for (int i = 0; i < steps; i++) {
    digitalWrite(Z_STEP_PIN, HIGH);
    delayMicroseconds(speed);
    digitalWrite(Z_STEP_PIN, LOW);
    delayMicroseconds(speed);
  }
}

void setServoPosition(Servo servo, int pos) {
  servo.write(pos);
}

void grip(){
  myStepper.step(stepsPerRotation);  
  delay(1000);  // Delay between rotations
}

void unGrip(){
  myStepper.step(-stepsPerRotation);  
  delay(1000);  // Delay between rotations
}
