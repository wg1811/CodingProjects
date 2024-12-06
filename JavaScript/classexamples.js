
//class example
class Person {
    constructor(name, age, salary) {
        this.name = name;
        this.age = age;
        this.salary = salary;
    }

    greeting() {
        //void 
        console.log(`Hi, my name is ${this.name}. My age is ${this.age}.`)
    }

    getSalary() {
        console.log(`${this.name}'s salary is ${this.salary} per year.`)
    }

    

}

const person1 = new Person("Bob", 23, 500000);
const person2 = new Person("Javiar", 36, 350000);
const person3 = new Person("Isabella", 54, 899000);

person1.greeting();
person2.greeting();
person3.greeting();

person1.getSalary();
person2.getSalary();
person3.getSalary();