import BankAccount from "./BankAccount.model.js";

let person1 = new BankAccount("Alex", 10000);
let person2 = new BankAccount("Mary", 13000);

console.log('Welcome to ' + BankAccount.info());


// fine
person1.deposit(1000);
person2.withdraw(10000);

// err
person1.deposit(-100);
person1.deposit('wjhfbewj');
person2.withdraw(45000);

person1.printStatments();
person2.printStatments();
