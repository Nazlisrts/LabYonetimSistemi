// ---- Yardımcı ----
function showSection(name) {
    document.querySelectorAll(".section").forEach(s => s.classList.remove("active"));
    document.getElementById("section-" + name).classList.add("active");
    if (name === "labs") loadLabs();
    if (name === "computers") loadComputers();
    if (name === "assign") { loadComputers(); loadStudents(); }
}

function showModal(id) {
    document.getElementById("labId").value = "";
    document.getElementById("labName").value = "";
    new bootstrap.Modal(document.getElementById(id)).show();
}

function logout() {
    localStorage.removeItem("lrp_user");
    window.location.href = "/";
}

// ---- LABS ----
async function loadLabs() {
    const res = await fetch("/api/labs");
    const labs = await res.json();
    document.getElementById("labTable").innerHTML = labs.map(l => `
        <tr>
            <td>${l.id}</td>
            <td>${l.name}</td>
            <td>${l.computers ? l.computers.length : 0}</td>
            <td><button class="btn btn-warning btn-sm" onclick="editLab(${l.id},'${l.name}')">Düzenle</button></td>
        </tr>`).join("");

    // PC modalındaki lab dropdown'ını da doldur
    const opts = labs.map(l => `<option value="${l.id}">${l.name}</option>`).join("");
    document.getElementById("pcLabId").innerHTML = opts;

    // Zimmet atama dropdown
    const pcRes = await fetch("/api/computers");
    const pcs = await pcRes.json();
    document.getElementById("aPcId").innerHTML =
        pcs.map(p => `<option value="${p.id}">${p.assetCode} - ${p.brand}</option>`).join("");
}

async function saveLab() {
    const id = document.getElementById("labId").value;
    const name = document.getElementById("labName").value;
    const method = id ? "PUT" : "POST";
    const url = id ? `/api/admin/labs/${id}` : "/api/admin/labs";
    await fetch(url, {
        method, headers: { "Content-Type": "application/json" },
        body: JSON.stringify({ name })
    });
    bootstrap.Modal.getInstance(document.getElementById("labModal")).hide();
    loadLabs();
}

function editLab(id, name) {
    document.getElementById("labId").value = id;
    document.getElementById("labName").value = name;
    new bootstrap.Modal(document.getElementById("labModal")).show();
}

// ---- COMPUTERS ----
async function loadComputers() {
    const res = await fetch("/api/computers");
    const pcs = await res.json();
    document.getElementById("pcTable").innerHTML = pcs.map(p => `
        <tr>
            <td>${p.assetCode}</td>
            <td>${p.brand}</td>
            <td>${p.processor}</td>
            <td>${p.ram} GB</td>
            <td>${p.hasHdmi ? "✅" : "❌"}</td>
            <td>${p.hasVeyon ? "✅" : "❌"}</td>
            <td><button class="btn btn-warning btn-sm" onclick="editPc(${p.id})">Düzenle</button></td>
        </tr>`).join("");
}

async function savePc() {
    const id = document.getElementById("pcId").value;
    const pc = {
        labId: parseInt(document.getElementById("pcLabId").value),
        brand: document.getElementById("pcBrand").value,
        processor: document.getElementById("pcProcessor").value,
        ram: parseInt(document.getElementById("pcRam").value),
        hasHdmi: document.getElementById("pcHdmi").checked,
        hasInternet: document.getElementById("pcInternet").checked,
        hasVeyon: document.getElementById("pcVeyon").checked,
    };
    const method = id ? "PUT" : "POST";
    const url = id ? `/api/admin/computers/${id}` : "/api/admin/computers";
    await fetch(url, {
        method, headers: { "Content-Type": "application/json" },
        body: JSON.stringify(pc)
    });
    bootstrap.Modal.getInstance(document.getElementById("pcModal")).hide();
    loadComputers();
}

async function editPc(id) {
    const res = await fetch("/api/computers");
    const pcs = await res.json();
    const p = pcs.find(x => x.id === id);
    document.getElementById("pcId").value = p.id;
    document.getElementById("pcBrand").value = p.brand;
    document.getElementById("pcProcessor").value = p.processor;
    document.getElementById("pcRam").value = p.ram;
    document.getElementById("pcHdmi").checked = p.hasHdmi;
    document.getElementById("pcInternet").checked = p.hasInternet;
    document.getElementById("pcVeyon").checked = p.hasVeyon;
    new bootstrap.Modal(document.getElementById("pcModal")).show();
}

// ---- STUDENTS ----
async function loadStudents() {
    const res = await fetch("/api/admin/students");
    const students = await res.json();
    document.getElementById("studentTable").innerHTML = students.map(s => `
        <tr><td>${s.fullName}</td><td>${s.studentNumber}</td><td>${s.computerId}</td></tr>`).join("");
}

async function assignStudent() {
    const student = {
        fullName: document.getElementById("aFullName").value,
        studentNumber: document.getElementById("aStudentNo").value,
        grade: parseInt(document.getElementById("aGrade").value),
        computerId: parseInt(document.getElementById("aPcId").value),
    };
    const res = await fetch("/api/admin/assign", {
        method: "POST", headers: { "Content-Type": "application/json" },
        body: JSON.stringify(student)
    });
    if (res.ok) {
        alert(`Zimmet atandı! Giriş bilgileri: ${student.studentNumber} / ${student.studentNumber}`);
        loadStudents();
    }
}

// Sayfa açılışında ilk bölümü göster
showSection("labs");