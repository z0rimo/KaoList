let elements = document.getElementsByClassName('password-blind-switch');

Array.from(elements).forEach(item => {
    (item as HTMLElement).onclick = () => {
        let result = item.classList.toggle("active");
        let password = item.parentElement.querySelector(".form-password-control");
        if (password instanceof HTMLInputElement) {
            password.type = result ? "text" : "password";
        }
    }
});
