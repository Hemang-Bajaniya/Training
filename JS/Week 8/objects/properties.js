// "use strict";

let bankAccount = {
    name: "HDFC",
    account_no: 323893972537,
    type: "saving",
    isActive: true,
    toString() {
        return `Bank:${this.name} no:${this.account_no} type:${this.type} isactive:${this.isActive}`;
    }
};

console.table(bankAccount);
console.table(bankAccount);

//read only descriptor
let desc = Object.getOwnPropertyDescriptor(bankAccount, "name");
// console.log(Object.getOwnPropertyDescriptors(desc));

console.log(desc);
// { value: 'HDFC', writable: true, enumerable: true, configurable: true }

Object.defineProperty(bankAccount, "toString", { writable: false, enumerable: false });

// op silently ignored in non strict mode
bankAccount.name = "SBI";

console.table(bankAccount);

console.log(bankAccount.toString());

// non configurable
console.log(Object.getOwnPropertyDescriptor(Math, "PI"));

// To be precise, non-configurability imposes several restrictions on
// 1. Can’t change
// configurable flag.
//  2. Can’t change
// enumerable flag.
//  3. Can’t change
// writable: false to
// defineProperty:
//  true (the other way round works).
// 4. Can’t change
// get/set for an accessor property (but can assign them if absent).


// Object.defineProperty(bankAccount, "account_no", { configurable: false });
// Cannot redefine property:
// Object.defineProperty(bankAccount, "account_no", { configurable: true, enumerable: true, writable: true });

Object.defineProperties(bankAccount, {
    customerName: { value: "John", writable: true, enumerable: true },
    surname: { value: "Smith", writable: true, enumerable: true },
    // ...
});

console.table(bankAccount);

Object.defineProperty(bankAccount, "name", { writable: false });

//cloning not clone prop flags 
let bankAccount2 = {};
Object.assign(bankAccount2, bankAccount);

// not even in structuredClone
bankAccount2 = structuredClone(bankAccount);

// this way works
bankAccount2 = Object.defineProperties({}, Object.getOwnPropertyDescriptors(bankAccount));
for (const key in bankAccount) {
    if (!Object.hasOwn(bankAccount, key)) continue;

    const element = bankAccount[key];

    bankAccount2[key] = element;
}

console.log(Object.getOwnPropertyDescriptors(bankAccount2));
console.table([Object.getOwnPropertyDescriptor(bankAccount, "name"), Object.getOwnPropertyDescriptor(bankAccount2, "name")]);


// getter/setter
Object.defineProperty(bankAccount, "fullName", {
    get: function () {
        return this.customerName + " " + this.surname;
    },
    set: function (val) {
        [this.customerName, this.surname] = val.split(" ");
    }
});

bankAccount.fullName = "Alex Lee";

console.log(bankAccount.fullName);

// bankAccount.account_no = 121212121222;

// console.table(bankAccount.account_no);