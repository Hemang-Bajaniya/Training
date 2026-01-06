class Transaction {
    constructor(type, amount, totalBalance) {
        this.type = type;
        this.amount = amount;
        this.totalBalance = totalBalance;
        this.at = new Date().toLocaleString();
    }

    toString() {
        return `Type:${this.type}, Amount:${this.amount}, Total balance:${this.totalBalance}, At:${this.at}`;
    }
}

// blurprint for all BankAccount obj
export default class BankAccount {
    // to initlize obj
    constructor(accountHolder, balance = 0) {
        this.accountHolder = accountHolder;
        this.balance = balance;
        this.transactions = [];
    }

    static info() {
        return "Bank System v1.0";
    }

    // to deposit amount
    deposit(amount) {
        try {
            // type chhecking
            if (typeof amount !== 'number') {
                throw new Error('Amount is not a number');
            }

            // validation
            if (amount <= 0) {
                throw new Error('Enter a positive amount to deposit');
            }

            this.balance += amount;
            this.transactions.push(new Transaction("deposit", amount, this.balance));
            console.log('Transcation for deposit done');
        } catch (error) {
            console.log('\nTranscation failed\n' + error.message);
        }
    }

    withdraw(amount) {
        try {
            // type checking
            if (typeof amount !== 'number') {
                throw new Error('Amount is not a number');
            }

            // validation
            if (amount <= 0) {
                throw new Error('Enter a positive amount to withdraw');
            }

            if (this.balance - amount < 0) {
                throw new Error('Insufficient balance to withdraw');
            }

            this.balance -= amount;
            this.transactions.push(new Transaction("withdraw", amount, this.balance));
            console.log('Transcation for withdraw done');

        } catch (error) {
            console.log('\nTranscation failed\n' + error.message);
        }
    }

    printStatments() {
        console.log(`\nAccount holder:${this.accountHolder}`);

        this.transactions.forEach(t => {
            console.log(t.toString());
        });

        console.log(`\nTotal balance:${this.balance}`);

    }
}

// let person1 = new BankAccount("Alex", 10000);
// let person2 = new BankAccount("Mary", 13000);

// console.log('Welcome to ' + BankAccount.info());


// // fine
// person1.deposit(1000);
// person2.withdraw(10000);

// // err
// person1.deposit(-100);
// person2.withdraw(45000);

// person1.printStatments();
// person2.printStatments();
