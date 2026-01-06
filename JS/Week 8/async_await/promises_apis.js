Promise.all([
    new Promise(resolve => setTimeout(() => resolve(1), 3000)), // 1 
    new Promise(resolve => setTimeout(() => resolve(2), 2000)),// 2 
    new Promise(resolve => setTimeout(() => resolve(3), 1000)) // 3
]).then(val => console.log(val));

let urls = [
    'https://api.github.com/users/iliakan',
    'https://apii.github.com/users/remy',
    'https://api.github.com/users/jeresig'
];

// map every url to the promise of the fetch
let requests = urls.map(url => fetch(url));

// Promise.all waits until all jobs are resolved 
Promise.allSettled(requests)
    .then(results => { // (*) 
        results.forEach((result, num) => {
            if (result.status == "fulfilled") {
                console.log(`${urls[num]}: ${result.value.status}`);
            }
            if (result.status == "rejected") {
                console.log(`${urls[num]}: ${result.reason}`);
            }
        });
    });

Promise.race([
    new Promise((resolve, reject) => setTimeout(() => resolve(1), 1000)),
    new Promise((resolve, reject) => setTimeout(() => reject(new Error("Whoops!")), 2000)),
    new Promise((resolve, reject) => setTimeout(() => resolve(3), 3000))
]).then(console.log); //