document.addEventListener("DOMContentLoaded", () => {
    const toggle = document.querySelector("[data-sidebar-toggle]");
    if (!toggle) return;

    const isMobile = window.matchMedia("(max-width: 767.98px)").matches;
    toggle.setAttribute("aria-expanded", String(!isMobile));

    toggle.addEventListener("click", () => {
        const isExpanded = toggle.getAttribute("aria-expanded") === "true";
        document.body.classList.toggle("sb-sidenav-toggled");
        toggle.setAttribute("aria-expanded", String(!isExpanded));
    });
});
