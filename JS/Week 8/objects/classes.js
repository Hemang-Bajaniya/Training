// blueprint for the obj
class User {
    constructor(name) {
        name;
    }

    introduce() {
        console.log(`Hello, my name is ${this.name}`);
    }
}

// class type is a function ref to its constructor
console.log(User);
console.log(User === User.prototype.constructor);

console.log(User.prototype.introduce());

console.log(Object.getOwnPropertyNames(User.prototype));

// can be rewritten as func declaration
// function User(name) {
//     this.name = name;
// }

// add properties
// User.prototype.introduce = function () {
//     console.log(this.name);
// };

// let user = new User("Alex");
// user.introduce();

// "Named Class Expression"
// (no such term in the spec, but that's similar to Named Function Expression)
// let User = class MyClass {
//     sayHi() {
//         alert(MyClass); // MyClass name is visible only inside the class
//     }
// };
// new User().sayHi(); // works, shows MyClass definition
// alert(MyClass);

class Employee {
    description = "Employee buleprint class";
    constructor(name) {
        this.name = name;
    }

    get name() {
        return this._name;
    }

    set name(value) {
        if (value.length < 4) {
            console.log("Invalid username");
            return;
        }
        this._name = value;
    }
}

let e = new Employee("Emp 1");
console.log(e.name);

console.log(Object.getOwnPropertyNames(Employee.prototype));

// class level prop shown in obj
console.log(Object.getOwnPropertyNames(e));


