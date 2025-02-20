import React, { useState } from 'react';
import { Form, Button, Container, Row, Col, Card, Alert } from 'react-bootstrap';
import { Formik } from 'formik';
import * as Yup from 'yup';
import './Login.css';

const validationSchema = Yup.object().shape({
  email: Yup.string()
    .email('Geçerli bir email adresi giriniz')
    .required('Email adresi zorunludur'),
  password: Yup.string()
    .min(6, 'Şifre en az 6 karakter olmalıdır')
    .required('Şifre zorunludur'),
});

const Login = () => {
  const [error, setError] = useState('');

  const handleSubmit = async (values, { setSubmitting }) => {
    try {
      // API çağrısı burada yapılacak
      console.log('Form değerleri:', values);
    } catch (err) {
      setError('Giriş yapılırken bir hata oluştu');
    } finally {
      setSubmitting(false);
    }
  };

  return (
    <Container className="login-container">
      <Row className="justify-content-center align-items-center min-vh-100">
        <Col md={6} lg={5}>
          <Card className="shadow-lg">
            <Card.Body className="p-5">
              <h2 className="text-center mb-4">Emlak Yönetim Sistemi</h2>
              
              {error && <Alert variant="danger">{error}</Alert>}

              <Formik
                initialValues={{ email: '', password: '', isAdmin: false, rememberMe: false }}
                validationSchema={validationSchema}
                onSubmit={handleSubmit}
              >
                {({
                  values,
                  errors,
                  touched,
                  handleChange,
                  handleBlur,
                  handleSubmit,
                  isSubmitting,
                }) => (
                  <Form onSubmit={handleSubmit}>
                    <Form.Group className="mb-3">
                      <Form.Label>Email</Form.Label>
                      <Form.Control
                        type="email"
                        name="email"
                        value={values.email}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        isInvalid={touched.email && errors.email}
                      />
                      <Form.Control.Feedback type="invalid">
                        {errors.email}
                      </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3">
                      <Form.Label>Şifre</Form.Label>
                      <Form.Control
                        type="password"
                        name="password"
                        value={values.password}
                        onChange={handleChange}
                        onBlur={handleBlur}
                        isInvalid={touched.password && errors.password}
                      />
                      <Form.Control.Feedback type="invalid">
                        {errors.password}
                      </Form.Control.Feedback>
                    </Form.Group>

                    <Form.Group className="mb-3">
                      <Form.Check
                        type="checkbox"
                        name="isAdmin"
                        label="Admin Girişi"
                        checked={values.isAdmin}
                        onChange={handleChange}
                      />
                    </Form.Group>

                    <Form.Group className="mb-4">
                      <Form.Check
                        type="checkbox"
                        name="rememberMe"
                        label="Beni Hatırla"
                        checked={values.rememberMe}
                        onChange={handleChange}
                      />
                    </Form.Group>

                    <Button
                      variant="primary"
                      type="submit"
                      className="w-100"
                      disabled={isSubmitting}
                    >
                      {isSubmitting ? 'Giriş yapılıyor...' : 'Giriş Yap'}
                    </Button>

                    <div className="text-center mt-3">
                      <a href="/forgot-password" className="text-decoration-none">
                        Şifremi Unuttum
                      </a>
                    </div>
                  </Form>
                )}
              </Formik>
            </Card.Body>
          </Card>
        </Col>
      </Row>
    </Container>
  );
};

export default Login; 