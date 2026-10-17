document.addEventListener("DOMContentLoaded", () => {
  const sidebarLinks = document.querySelectorAll(".admin-nav a");

  sidebarLinks.forEach((link) => {
    link.addEventListener("click", () => document.body.classList.remove("sidebar-open"));
  });
});
