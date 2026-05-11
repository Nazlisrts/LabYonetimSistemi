// Her korumalı sayfanın en üstüne: <script src="/js/auth.js"></script>
function requireRole(role) {
    const raw = localStorage.getItem("lrp_user");
    if (!raw) { window.location.href = "/"; return null; }
    const user = JSON.parse(raw);
    if (user.role !== role) { window.location.href = "/"; return null; }
    return user;
}