# Rudal Iran - 2D AI Game Prototype

Rudal Iran adalah purwarupa game 2D dengan pergerakan layar statis yang berfokus pada implementasi berbagai jenis Kecerdasan Buatan (AI). Pemain mengendalikan seekor burung yang harus bertahan hidup dari kejaran rudal pintar (Homing Missile) dan Smart Drone yang mampu mencari jalan melewati rintangan.

Proyek ini dibuat untuk mata kuliah AI for Game at Ubaya Informatics

# Cara Bermain

- Kontrol Player: Gunakan tombol W, A, S, D untuk menggerakkan burung.
- Tujuan: Hindari rintangan (pipa) dan musuh yang terus mengejar.
- Power-Ups:
  🛡️ Shield: Membuat rudal yang mendekat menjadi ketakutan dan berbalik arah.
  🌀 Confusion (Signal Jammer): Mengacak sinyal rudal sehingga bergerak secara acak/kacau.

# Implementasi Artificial Intelligence (AI)

Game ini mengimplementasikan 3 kategori utama AI sesuai dengan kriteria proyek:

1. Movement AI (Rudal)
   Rudal memiliki sistem pergerakan mandiri yang bereaksi terhadap pemain dan power-up yang ada di arena.

Steering Seek: Secara default, rudal berfungsi sebagai homing missile yang muncul dari sisi layar, mendeteksi posisi Y pemain, dan terus bergerak memburu pemain.

Steering Flee: Terpicu ketika pemain mengambil power-up Shield. Rudal yang tadinya mengejar akan langsung bergerak ke arah berlawanan untuk menjauhi pemain.

Wandering: Terpicu ketika pemain mengambil power-up Confusion. Sistem navigasi rudal akan rusak sementara, membuatnya bergerak ke arah yang acak/kacau.

2. Pathfinding AI (Smart Drone)
   Smart Drone adalah musuh spesial yang muncul secara berkala dan bisa menghindari rintangan (pipa) secara cerdas.

Algoritma A (A-Star) Custom:\* Sistem membagi layar menjadi grid virtual untuk membaca area mana yang kosong dan mana yang terhalang pipa. AI menghitung jalur paling optimal dan presisi menuju pemain tanpa menggunakan library eksternal.

Visualisasi Path: Rute yang telah dikalkulasi oleh algoritma A\* divisualisasikan dalam bentuk titik-titik laser merah (hologram) di layar sebagai panduan rute.

Movement Path: Drone tidak bergerak lurus menabrak rintangan, melainkan bergerak melintasi titik demi titik path A\* tersebut secara bertahap dan berbelok melewati celah pipa hingga mencapai target.

3. Decision Making AI (Rudal FSM)
   Logika pengambilan keputusan rudal diatur menggunakan Finite State Machine (FSM). Rudal memiliki berbagai state (status) yang berubah secara dinamis berdasarkan situasi permainan:

- State [Attack]: Kondisi dasar/normal. Selama pemain tidak memiliki buff, rudal akan terus mengeksekusi perintah Seek (menyerang).

- State [Dodge]: Terjadi transisi ke state ini jika pemain mengambil item Shield. Rudal akan mengubah perilakunya menjadi Flee.

- State [Confused]: Terjadi transisi ke state ini jika pemain mengambil item Confusion. Rudal akan mengubah perilakunya menjadi Wandering.

# Instalasi & Cara Menjalankan

1. Clone repository ini: git clone [URL_GITHUB_KAMU]
2. Buka project menggunakan game engine (misal: Unity / Godot).
3. Buka scene utama (misal: MainScene.unity).
4. Tekan tombol Play untuk mencoba purwarupa game.

# Credits & Assets

- Semua asset visual dan audio yang digunakan dalam proyek ini bersifat free-to-use / royalty-free.
- Player Sprite: [Sebutkan Sumber]
- Missile & Drone Sprite: [Sebutkan Sumber]
- Background & Environment: [Sebutkan Sumber]
- Audio: [Sebutkan Sumber]
