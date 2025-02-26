import axios from 'axios';
import authService from '../../services/authService';

// Axios mock'unu oluştur
jest.mock('axios');

describe('Auth Service Tests', () => {
  const mockUserData = {
    firstName: 'Test',
    lastName: 'Kullanıcı',
    email: 'test@example.com',
    password: '123456',
    phone: '5551234567'
  };

  beforeEach(() => {
    // Her testten önce mock'ları temizle
    jest.clearAllMocks();
  });

  test('başarılı kayıt işlemi', async () => {
    // Mock başarılı yanıt
    const mockResponse = { data: { message: 'Kullanıcı başarıyla kaydedildi' } };
    axios.post.mockResolvedValueOnce(mockResponse);

    const response = await authService.register(mockUserData);
    
    // API çağrısının doğru parametrelerle yapıldığını kontrol et
    expect(axios.post).toHaveBeenCalledWith(
      'https://localhost:7777/api/auth/register',
      {
        firstName: mockUserData.firstName,
        lastName: mockUserData.lastName,
        email: mockUserData.email,
        password: mockUserData.password,
        phoneNumber: mockUserData.phone
      }
    );

    // Yanıtın doğru olduğunu kontrol et
    expect(response).toEqual(mockResponse.data);
  });

  test('başarısız kayıt işlemi - sunucu hatası', async () => {
    // Mock hata yanıtı
    const errorMessage = 'Kayıt işlemi başarısız oldu';
    axios.post.mockRejectedValueOnce({
      response: { data: { message: errorMessage } }
    });

    await expect(authService.register(mockUserData))
      .rejects
      .toThrow(errorMessage);
  });

  test('başarısız kayıt işlemi - bağlantı hatası', async () => {
    // Mock network hatası
    axios.post.mockRejectedValueOnce(new Error('Network Error'));

    await expect(authService.register(mockUserData))
      .rejects
      .toThrow('Sunucu ile bağlantı kurulamadı');
  });
}); 