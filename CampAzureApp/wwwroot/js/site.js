const mainFileInput = document.getElementById("File");
const emailInput = document.getElementById("UserEmail");
const customButton = document.getElementsByClassName("custom-button")[0];
const customText = document.getElementsByClassName("custom-text")[0];
const submitButton = document.getElementById("submit-form-button");

customButton.addEventListener("click", () => {
    mainFileInput.click();
});

emailInput.addEventListener("input", (e) => {
    if (mainFileInput.value && e.target.value.length >= 5 && e.target.value.includes("@")) {
        submitButton.disabled = false;
    } else {
        submitButton.disabled = true;
    }
});

mainFileInput.addEventListener("change", () => {
    if (mainFileInput.value) {
        const fileName = mainFileInput.files[0].name;
        const fileExtension = fileName.split('.').pop();
        if (fileExtension != "docx") {
            changeTextColor(customText, true);
            customText.textContent = "You can upload only .docx files"
            return;
        }
        changeTextColor(customText, false);
        customText.textContent = fileName;
        submitButton.disabled = !!!emailInput.value;
    } else {
        changeTextColor(customText, false);

        customText.textContent = "You haven't chosen a document, yet";
        submitButton.disabled = true;
    }
});

function changeTextColor(element, isError) {
    if (isError) {
        element.style.color = "red";
    } else {
        element.style.color = "#aaa";
    }
}