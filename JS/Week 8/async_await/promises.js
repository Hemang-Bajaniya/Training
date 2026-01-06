let promise = new Promise(function (resolve, reject) {
    // the function is executed automatically when the promise is constructed 
    // after 1 second signal that the job is done with the result "done" 
    setTimeout(() => resolve("done"), 1000);
    setTimeout(() => reject(new Error("Error")), 1000);

    // reject(new Error("…")); // ignored 
    // setTimeout(() => resolve("…")); // ignored
});

//  subscribing events
// promise.then(resolve callback, reject callback);

// promise.then(val => console.log(val), err => console.log(err.message));

// only in succ
// promise.then(val => console.log(val));

// only in fails
// promise.catch(e => console.log(e));

function loadScript(path) {
    return new Promise((resolve, reject) => {
        let script = document.createElement('script');
        script.src = path;

        script.onload = () => resolve();
        script.onerror = () => reject(new Error('Err in loading script'));

        document.head.appendChild(script);
    });
}

promise = loadScript('https://cdnjs.cloudflare.com/ajax/libs/moment.js/2.30.1/moment.min.js');

promise.then(_ => {
    alert(moment().format('MMMM Do YYYY, h:mm:ss a'));
}, err => {
    alert(err.message);
});

promise.then(_ => {
    alert('another habndler');
});
