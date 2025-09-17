public class Main {
    public static void main(String[] args) {
        Vechicle v = new Car();
        v.run();
    }
}

class Vechicle {
    public void run() {
        System.out.println("I'm running!");
    }
}

class Car extends Vechicle {
    @Override   //伪代码，验证重写
    public void run() {
        System.out.println("Car is running!");
    }
}