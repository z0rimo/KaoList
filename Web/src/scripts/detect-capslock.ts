let input = document.getElementById("pw");
let text = document.getElementById("capslock");

input.addEventListener("keyup",
    function (event) {

        if (event.getModifierState("CapsLock")) {
            text.style.display = "block";
        } else {
            text.style.display = "none";
        }
    }
);