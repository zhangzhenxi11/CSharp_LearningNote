class Vehicle:
    def run(self):
        print("I'm running!")
class Car(Vehicle):
    def run(self):
        print("Car is running!")
class RaseCar(Car):
    def run(self):
        print("RaseCar is running!")

v = Vehicle();
v.run();
print("=====")

v = RaseCar();
v.run();
print("=====")
