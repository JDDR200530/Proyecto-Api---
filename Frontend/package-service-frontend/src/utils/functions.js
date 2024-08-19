export const showSuccessAlert = (message = '¡Éxito! La operación se ha completado correctamente.') => {
    const alert = document.createElement('div');
    alert.className = 'fixed top-4 right-4 bg-green-500 text-white p-4 rounded-md shadow-lg';
    alert.textContent = message;
    
    document.body.appendChild(alert);
    
    setTimeout(() => {
      alert.remove();
    }, 3000); // Desaparecerá después de 3 segundos
  };
  
  export const showErrorAlert = (message = 'Error: Ha ocurrido un problema. Por favor, intenta de nuevo.') => {
    const alert = document.createElement('div');
    alert.className = 'fixed top-4 right-4 bg-red-500 text-white p-4 rounded-md shadow-lg';
    alert.textContent = message;
    
    document.body.appendChild(alert);
    
    setTimeout(() => {
      alert.remove();
    }, 3000); // Desaparecerá después de 3 segundos
  };
  