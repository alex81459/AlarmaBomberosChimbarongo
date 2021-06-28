const int pinRele = 2;
void setup()
{
   Serial.begin(9600);
   pinMode(pinRele, OUTPUT);
}
void loop() {
  if (Serial.available() > 0)
   {
      int option = Serial.read();
      if (option == 'a')
      {
         digitalWrite(pinRele, LOW);      
      }
      if (option == 'b')
      {
         digitalWrite(pinRele, HIGH);
      }
   }
}
