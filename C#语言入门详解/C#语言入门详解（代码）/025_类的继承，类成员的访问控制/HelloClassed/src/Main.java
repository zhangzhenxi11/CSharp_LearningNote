public class Main {
    public static void main(String[] args) {
        Car car = new Car();
        car.owner = "Thimothy";
        System.out.print(car.owner);
    }
}
class Vehicle{
    public String owner;
}
class Car extends Vehicle{

}