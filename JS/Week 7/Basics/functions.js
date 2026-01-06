function showMessage(from, text = "No message") {

    console.log(from ?? "None" + ': ' + text);
}

let from = "Ann";

showMessage(); // *Ann*: Hello

// the value of "from" is the same, the function modified a local copy
console.log(from); // Ann

// callback fn

function ask(question, yes, no) {
    if (10 > 20)
        yes();
    else
        no();
}

ask("Do you want to make this payemnt?", completePayment, cancelPayment);

// moved on top as hoisting
function completePayment() {
    console.log("Payment completed");
};

// func declared as var
// define when exe reaches here
// const completePayment = function () {
//     console.log("Payment completed");
// };

function cancelPayment() {
    console.log("Payment failed");
}

const toPowerOf2 = i => 2 ** i; // single args
const toBasePower = (b, i) => b ** i; // multi args
const hello = () => "Hello"; // empty args

for (let i = 0; i <= 10; i++) {
    console.log(`2^${i} = ${toPowerOf2(i)}`);

}

