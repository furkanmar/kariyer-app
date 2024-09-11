<template>
    <div class="background-container">
      <div class="overlay">
        <h2>BAŞARIYA GİDEN MERDİVENLERİ  </h2>
        <h2> KARİYER-APP</h2>
        <h2>İLE TIRMANIN</h2>
      </div>
    </div>
  </template>
  
  <script>
  export default {

  }
  </script>
  

  <style scoped>
  .background-container {
    background-image: url('@/../public/stairs.jpg');
    background-size: cover;
    background-position: center;
    width: 100%;
    height: 100vh; /* Tam ekran yüksekliği */
    display: flex;
    align-items: center;
    justify-content: center;
    position: relative;
  }
  
  .overlay {
  background-color: rgba(0, 0, 0, 0.5); /* Yarı saydam siyah arka plan */
  padding: 1rem 2rem; /* Üst ve alt boşluk */
  border-radius: 8px;
  text-align: center;
  color: white;
  font-weight: bold; /* Kalın yazı tipi */
  position: absolute;
  top: 10%; /* Resmin üst kısmından biraz boşluk */
  left: 50%;
  transform: translate(-50%, 0); /* Orta hizalama */
}

h2 {
  margin: 0;
  font-size: 2rem; /* Font boyutu */
}
  </style>