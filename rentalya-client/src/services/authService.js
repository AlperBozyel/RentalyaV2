import axios from 'axios';

const API_URL = 'https://localhost:7777/api'; // API'nizin URL'ini buraya yazın

const authService = {
  register: async (userData) => {
    try {
      const response = await axios.post(`${API_URL}/auth/register`, {
        firstName: userData.firstName,
        lastName: userData.lastName,
        email: userData.email,
        password: userData.password,
        phoneNumber: userData.phone
      });
      return response.data;
    } catch (error) {
      if (error.response) {
        throw new Error(error.response.data.message || 'Kayıt işlemi başarısız oldu');
      }
      throw new Error('Sunucu ile bağlantı kurulamadı');
    }
  }
};

export default authService; 