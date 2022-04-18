const items = Array.from(document.querySelectorAll<HTMLInputElement>("input[type=checkbox].accept-item"));
let requiredItems = items.filter(item => item.hasAttribute('required'));
let submit = document.querySelector<HTMLInputElement | HTMLButtonElement>("*[type=submit]");
const acceptAllButton = document.querySelector<HTMLInputElement>("input[type=checkbox].accept-all");

function updateCheck() {
    let allChecked = requiredItems.reduce((acc, current) => acc && current.checked, true);
    submit.disabled = !allChecked;
    acceptAllButton.checked = allChecked;
}

requiredItems.forEach(item => {
    item.onchange = updateCheck;
});

acceptAllButton.onchange = (evt) => {
    let target = evt.currentTarget as HTMLInputElement;
    if (target.checked) {
        requiredItems.forEach(item => {
            if (!item.checked) {
                item.click();
            }
        })
    }
}

updateCheck();