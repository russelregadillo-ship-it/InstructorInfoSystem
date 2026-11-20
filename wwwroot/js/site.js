document.addEventListener("DOMContentLoaded", function () {

    const savedTheme = localStorage.getItem("instructorTheme") || "light";
    document.documentElement.setAttribute("data-theme", savedTheme);

    const themeButton = document.createElement("button");
    themeButton.id = "themeToggle";
    themeButton.type = "button";
    document.body.appendChild(themeButton);

    function updateThemeButton() {
        const currentTheme = document.documentElement.getAttribute("data-theme");

        themeButton.textContent =
            currentTheme === "dark" ? "☀️ Light Mode" : "🌙 Dark Mode";
    }

    themeButton.addEventListener("click", function () {
        const currentTheme = document.documentElement.getAttribute("data-theme");
        const newTheme = currentTheme === "dark" ? "light" : "dark";

        document.documentElement.setAttribute("data-theme", newTheme);
        localStorage.setItem("instructorTheme", newTheme);

        updateThemeButton();
    });

    updateThemeButton();
});

