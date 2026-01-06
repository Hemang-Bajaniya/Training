class User {
    static institute = "Training";
    _age = null;

    constructor(name, age) {
        this.name = name;
        this.age = age;
    }

    get age() {
        return this._age;
    }

    set age(value) {
        if (value < 0)
            throw new Error("Invalid age");

        this._age = value;
    }

    login() {
        console.log(`User ${this.name} has logged in`);
        return this;
    }

}

class Student extends User {

    /* if no constructor defined
    constructor(...args) { 
        super(...args); 
    }*/

    constructor(name, age, classOf, rollNo) {
        super(name, age);
        this.classOf = classOf;
        this.rollNo = rollNo;
    }

    // static prop func
    static classComperor(s1, s2) {
        return s1.classOf - s2.classOf;
    }

    // overriding
    // extend behaviour not replace
    login() {
        super.login();
        console.log(`User ${this.name} is a student`);
        return this;
    }

    register() {
        console.log(`Roll no ${this.rollNo} has registred for ${this.classOf} class`);
        return this;
    }
}

// let s = new Student("Alex", "123", 12, "7", 102);
// s.login().register();

let selection = [new Student("Alex", 12, 7, 102), new Student("Lee", 10, 5, 432), , new Student("Jhon", 18, 10, 394)];
selection.sort(Student.classComperor);
console.table(selection);

class CoffeeMachine {
    #waterAmount = 0;
    get waterAmount() {
        return this.#waterAmount;
    }
    set waterAmount(value) {
        if (value < 0) throw new Error("Negative water");
        this.#waterAmount = value;
    }
}
let machine = new CoffeeMachine();
machine.waterAmount = 100;
console.log(machine.waterAmount); // Error

console.log(Object.getOwnPropertyDescriptors(CoffeeMachine.prototype));

// extending built-ins

class MyArray extends Array {
    replicate(n = 1) {
        let original = [...this];
        for (let index = 0; index < n; index++) {
            this.push(...original);
        }
    }
}
class MyArray2 extends Array {
    replicate(n = 1) {
        let original = [...this];
        for (let index = 0; index < n; index++) {
            this.push(...original);
        }
    }
}

let arr = new MyArray(1, 2, 5, 10, 50);
console.log(arr);
arr.replicate(3);
console.log(arr);

// Class checking: "instanceof"
//check prototypes
console.log(arr instanceof Object, arr instanceof MyArray, arr instanceof Array, arr instanceof MyArray2);
