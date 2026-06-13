const API = "http://link-vault.runasp.net/api";

const Auth = {
  setToken(token) {
    localStorage.setItem("token", token);
  },
  getToken() {
    return localStorage.getItem("token");
  },
  clear() {
    localStorage.removeItem("token");
  },
  isLoggedIn() {
    return !!this.getToken();
  },
  getEmail() {
    try {
      const token = this.getToken();
      if (!token) return "";
      const base64Url = token.split(".")[1];
      const base64 = base64Url.replace(/-/g, "+").replace(/_/g, "/");
      const payload = JSON.parse(window.atob(base64));
      return (
        payload[
          "http://schemas.xmlsoap.org/ws/2005/05/identity/claims/emailaddress"
        ] ||
        payload["email"] ||
        ""
      );
    } catch (e) {
      return "";
    }
  },
  requireAuth() {
    if (!this.isLoggedIn()) {
      window.location.href = "login.html";
    }
  },

  redirectIfLoggedIn() {
    if (this.isLoggedIn()) {
      window.location.href = "bookmarks.html";
    }
  },
};

async function apiFetch(path, options = {}) {
  const headers = { "Content-Type": "application/json" };
  const token = Auth.getToken();
  if (token) headers["Authorization"] = `Bearer ${token}`;

  // ✅ just one line, no nested function
  const res = await fetch(API + path, { ...options, headers });

  if (res.status === 401) {
    Auth.clear();
    window.location.href = "login.html";
    return null;
  }

  if (res.status === 204) return null;

  const data = await res.json().catch(() => null);

  if (!res.ok) {
    let msg = data?.message || data?.title || "Error " + res.status;
    if (data?.errors) msg = Object.values(data.errors).flat().join(" ");
    throw new Error(msg);
  }

  return data;
}

function initNavbar(activePage) {
  const navPlaceHolder = document.getElementById("navbar-placeholder");
  if (!navPlaceHolder) return;

  const navLinks = [
    { id: "categories", title: "Categories", url: "category.html" },
    { id: "bookmarks", title: "Bookmarks", url: "bookmarks.html" },
    { id: "bookmark", title: "bookmark", url: "bookmark.html" },
    { id: "notes", title: "Notes", url: "note.html" },
  ];

  navPlaceHolder.innerHTML = `
    <nav class="navbar navbar-expand-lg navbar-dark bg-dark shadow mb-4">
      <div class="container">
        <a class="navbar-brand fw-bold" href="bookmarks.html">
          <i class="bi bi-vault"></i> LinkVault
        </a>
        <button class="navbar-toggler" type="button" data-bs-toggle="collapse" data-bs-target="#mainNav">
          <span class="navbar-toggler-icon"></span>
        </button>
        <div class="collapse navbar-collapse" id="mainNav">
          <ul class="navbar-nav me-auto mb-2 mb-lg-0">
            ${navLinks
              .map(
                (link) => `
              <li class="nav-item">
                <a class="nav-link ${activePage === link.id ? "active fw-bold border-bottom border-primary" : ""}"
                   href="${link.url}">${link.title}</a>
              </li>
            `,
              )
              .join("")}
          </ul>
          <div class="d-flex align-items-center">
            <span class="navbar-text me-3 text-info small">
              <i class="bi bi-person-circle"></i> ${Auth.getEmail()}
            </span>
            <button class="btn btn-outline-danger btn-sm" onclick="logout()">Logout</button>
          </div>
        </div>
      </div>
    </nav>`;
}

function logout() {
  Auth.clear();
  window.location.href = "login.html";
}

function showAlert(message, type = "success") {
  const alertArea = document.getElementById("alert-area");
  if (!alertArea) return;

  const id = "alert-" + Date.now();
  alertArea.innerHTML = `
    <div id="${id}" class="alert alert-${type} alert-dismissible fade show" role="alert">
      ${message}
      <button type="button" class="btn-close" data-bs-dismiss="alert"></button>
    </div>`;

  setTimeout(() => {
    const el = document.getElementById(id);
    if (el) {
      const bsAlert = bootstrap.Alert.getOrCreateInstance(el);
      bsAlert.close();
    }
  }, 4000);
}

function esc(text) {
  const div = document.createElement("div");
  div.textContent = text ?? "";
  return div.innerHTML;
}

function fmtDate(dateStr) {
  if (!dateStr) return "—";
  return new Date(dateStr).toLocaleDateString("en-US", {
    year: "numeric",
    month: "short",
    day: "numeric",
  });
}
