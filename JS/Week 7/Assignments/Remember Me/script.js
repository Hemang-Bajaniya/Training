const banner = document.getElementById("banner");
const accept_consent_btn = document.getElementById("accept_consent_btn");
const user_frm = document.getElementById("user_frm");
const user = document.getElementById("user");
const session_count = document.getElementById("session_count");
const count_btn = document.getElementById("count_btn");
let sessionCounts = 0;

document.addEventListener("DOMContentLoaded", (e) => {
    const userName = localStorage.getItem('userName');
    if (userName != null)
        user.value = userName;

    sessionCounts = sessionStorage.getItem("sessionCounts") ?? 0;
    session_count.textContent = `total session count ${sessionCounts}`;

    console.log(document.cookie.includes("consent=true"));


    if (document.cookie.includes("consent=true"))
        banner.style.display = "none";
});

user_frm.addEventListener("submit", (e) => {
    e.preventDefault();

    const userName = user.value.trim();
    localStorage.setItem("userName", userName);
    user.value = userName;

});

accept_consent_btn.addEventListener("click", (e) => {
    let expires = new Date();
    expires.setTime(expires.getTime() + (7 * 24 * 60 * 60 * 1000));
    document.cookie = "consent=true;  expires=" + expires.toUTCString() + ";";
    banner.style.display = "none";
});

count_btn.addEventListener("click", (e) => {
    sessionCounts = sessionStorage.getItem("sessionCounts");
    sessionCounts++;
    sessionStorage.setItem("sessionCounts", sessionCounts);
    session_count.textContent = `total session count ${sessionCounts}`;
});