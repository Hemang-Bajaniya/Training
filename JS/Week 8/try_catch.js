console.log('start');

try {
    console.log(hello);

} catch (error) {
    console.log("Error");
    // console.log(error.name, error.message, error.stack);
}
// catch{
//     console.log('err');

// }
// console.log(hello);

console.log('end');


try {
    // err in async code will not be captured
    // as engine already left that block
    setTimeout(() => {
        // console.log(hello);

    }, 1000);
} catch (error) {
    console.log('Error in settimeout', typeof (error));
}

console.log('last one');


let malformedJson = '{ "name":"alex", "age":10}';

try {
    let obj = JSON.parse(malformedJson);

    if (!('name' in obj))
        throw new Error('Name is not provided');

    if (typeof (obj['age']) !== 'number')
        throw new Error('Age is not a number');

    console.log(obj);


} catch (error) {
    console.log('Invalid json:', error.message);

}

function func() {
    // start doing something that needs completion (like measurements) 
    try {
        // ... 
    } finally {
        // complete that thing even if all dies 
    }
}
