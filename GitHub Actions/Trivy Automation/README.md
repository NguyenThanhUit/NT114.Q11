# 🔒 Trivy Security Scan Report

**Scan Date:** 01.19.2026
**Repository:** nguyenthanh91ndu/*

---

## 📦 Vulnerability Summary by Service

| Service | 🔴 Critical | 🟠 High | 🟡 Medium | 🟢 Low | Total |
|---------|-------------|----------|-----------|---------|-------|
| **auction** | 0 | 5 | 4 | 0 | 9 |
| **auction** | 0 | 5 | 4 | 0 | 9 |
| **bidding** | 0 | 5 | 4 | 0 | 9 |
| **bidding** | 0 | 5 | 4 | 0 | 9 |
| **buying** | 0 | 5 | 4 | 0 | 9 |
| **buying** | 0 | 5 | 4 | 0 | 9 |
| **deposits** | 0 | 5 | 4 | 0 | 9 |
| **deposits** | 0 | 5 | 4 | 0 | 9 |
| **gateway** | 0 | 5 | 4 | 0 | 9 |
| **gateway** | 0 | 5 | 4 | 0 | 9 |
| **identity** | 0 | 5 | 5 | 1 | 11 |
| **identity** | 0 | 5 | 5 | 1 | 11 |
| **notify** | 0 | 5 | 4 | 0 | 9 |
| **notify** | 0 | 5 | 4 | 0 | 9 |
| **order** | 0 | 5 | 4 | 0 | 9 |
| **order** | 0 | 5 | 4 | 0 | 9 |
| **search** | 0 | 0 | 0 | 0 | 0 |
| **search** | 0 | 0 | 0 | 0 | 0 |
| **user** | 0 | 5 | 4 | 0 | 9 |
| **user** | 0 | 5 | 4 | 0 | 9 |
| **web** | 1 | 5 | 4 | 2 | 12 |
| **web** | 1 | 5 | 4 | 2 | 12 |
|---------|-------------|----------|-----------|---------|-------|
| **TOTAL** | **2** | **100** | **82** | **6** | **190** |

---

## 📋 Detailed Vulnerability Reports

### 🔍 auction-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/auction-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 bidding-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/bidding-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 buying-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/buying-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 deposits-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/deposits-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 gateway-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/gateway-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 identity-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/identity-svc:latest

**Summary:** 11 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 5
- 🟢 **LOW:** 1

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (5 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-39694** (Duende.IdentityServer 7.0.4 → 7.0.6, 6.3.10, 6.2.5, 6.1.8, 6.0.5)

</details>

#### 🟢 LOW Vulnerabilities

<details>
<summary>Click to expand (1 issues)</summary>

- **CVE-2024-49755** (Duende.IdentityServer 7.0.4 → 7.0.8)

</details>

---

### 🔍 notify-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/notify-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 order-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/order-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 search-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/search-svc:latest

**Summary:** 0 vulnerabilities found

✅ **No vulnerabilities found!**

---

### 🔍 user-svc

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/user-svc:latest

**Summary:** 9 vulnerabilities found

- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4

#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-68973** - gpgv
- **Installed:** 2.2.40-1.1+deb12u1
- **Fixed:** 2.2.40-1.1+deb12u2
- **Description:** In GnuPG before 2.4.9, armor_filter in g10/armor.c has two increments of an index variable where one is intended, leading to an out-of-bounds write fo...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-68973

**CVE-2025-6020** - libpam-modules
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-modules-bin
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam-runtime
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020

**CVE-2025-6020** - libpam0g
- **Installed:** 1.5.2-6+deb12u1
- **Fixed:** 1.5.2-6+deb12u2
- **Description:** A flaw was found in linux-pam. The module pam_namespace may use access user-controlled paths without proper protection, allowing local users to elevat...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-6020


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2024-22365** (libpam-modules 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-modules-bin 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam-runtime 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)
- **CVE-2024-22365** (libpam0g 1.5.2-6+deb12u1 → 1.5.2-6+deb12u2)

</details>

---

### 🔍 web-app

**Scan Date:** 01.19.2026
**Full Image:** nguyenthanh91ndu/web-app:latest

**Summary:** 12 vulnerabilities found

- 🔴 **CRITICAL:** 1
- 🟠 **HIGH:** 5
- 🟡 **MEDIUM:** 4
- 🟢 **LOW:** 2

#### 🔴 CRITICAL Vulnerabilities

**CVE-2025-55182** - next
- **Installed:** 15.2.3
- **Fixed:** 15.0.5, 15.1.9, 15.2.6, 15.3.6, 15.4.8, 15.5.7, 16.0.7
- **Description:** A pre-authentication remote code execution vulnerability exists in React Server Components versions 19.0.0, 19.1.0, 19.1.1, and 19.2.0 including the following packages: react-server-dom-parcel, react-...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-55182


#### 🟠 HIGH Vulnerabilities (Top 10)

**CVE-2025-64756** - glob
- **Installed:** 10.4.5
- **Fixed:** 11.1.0, 10.5.0
- **Description:** Glob matches files using patterns the shell uses. Starting in version 10.2.0 and prior to versions 10.5.0 and 11.1.0, the glob CLI contains a command ...
- **Reference:** https://avd.aquasec.com/nvd/cve-2025-64756

**GHSA-mwv6-3258-q52c** - next
- **Installed:** 15.2.3
- **Fixed:** 14.2.34, 15.0.6, 15.1.10, 15.2.7, 15.3.7, 15.4.9, 15.5.8, 15.6.0-canary.59, 16.0.9, 16.1.0-canary.17
- **Description:** A vulnerability affects certain React packages for versions 19.0.0, 19.0.1, 19.1.0, 19.1.1, 19.1.2, 19.2.0, and 19.2.1 and frameworks that use the aff...
- **Reference:** https://github.com/advisories/GHSA-mwv6-3258-q52c

**CVE-2026-23745** - tar
- **Installed:** 6.2.1
- **Fixed:** 7.5.3
- **Description:** node-tar is a Tar for Node.js. The node-tar library (<= 7.5.2) fails to sanitize the linkpath of Link (hardlink) and SymbolicLink entries when preserv...
- **Reference:** https://avd.aquasec.com/nvd/cve-2026-23745

**CVE-2026-23745** - tar
- **Installed:** 7.4.3
- **Fixed:** 7.5.3
- **Description:** node-tar is a Tar for Node.js. The node-tar library (<= 7.5.2) fails to sanitize the linkpath of Link (hardlink) and SymbolicLink entries when preserv...
- **Reference:** https://avd.aquasec.com/nvd/cve-2026-23745

**CVE-2026-23745** - tar
- **Installed:** 7.4.3
- **Fixed:** 7.5.3
- **Description:** node-tar is a Tar for Node.js. The node-tar library (<= 7.5.2) fails to sanitize the linkpath of Link (hardlink) and SymbolicLink entries when preserv...
- **Reference:** https://avd.aquasec.com/nvd/cve-2026-23745


#### 🟡 MEDIUM Vulnerabilities

<details>
<summary>Click to expand (4 issues)</summary>

- **CVE-2025-55173** (next 15.2.3 → 14.2.31, 15.4.5)
- **CVE-2025-57752** (next 15.2.3 → 14.2.31, 15.4.5)
- **CVE-2025-57822** (next 15.2.3 → 14.2.32, 15.4.7)
- **GHSA-w37m-7fhw-fmv9** (next 15.2.3 → 15.0.6, 15.1.10, 15.2.7, 15.3.7, 15.4.9, 15.5.8, 15.6.0-canary.59, 16.0.9, 16.1.0-canary.17)

</details>

#### 🟢 LOW Vulnerabilities

<details>
<summary>Click to expand (2 issues)</summary>

- **GHSA-73rr-hh4g-fpgx** (diff 5.2.0 → 8.0.3)
- **CVE-2025-30218** (next 15.2.3 → 12.3.6, 13.5.10, 14.2.26, 15.2.4)

</details>

---


---

## 📊 How to Read This Report

- **JSON Files:** Raw scan data in `scan-*-*.json`
- **Severity Levels:**
  - 🔴 **CRITICAL:** Fix immediately - Known exploits exist
  - 🟠 **HIGH:** Fix as soon as possible - High risk
  - 🟡 **MEDIUM:** Plan to fix - Moderate risk
  - 🟢 **LOW:** Fix when convenient - Low risk

## 🔍 Next Steps

1. Review CRITICAL and HIGH vulnerabilities immediately
2. Check if fixes are available in the `FixedVersion` column
3. Update base images and dependencies
4. Re-run security scans after updates

---

_Generated by GitHub Actions Trivy Scanner_
