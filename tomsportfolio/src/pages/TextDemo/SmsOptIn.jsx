import React, { useState } from 'react';
import axios from 'axios';
import { useNavigate } from 'react-router-dom';

const SmsOptIn = () => {
  const [formData, setFormData] = useState({
    name: '',
    phoneNumber: '',
    countryCode: '1', // Default to US
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const navigate = useNavigate(); // Import useNavigate

  const handleInputChange = (e) => {
    const { name, value } = e.target;
    setFormData(prev => ({
      ...prev,
      [name]: value
    }));
  };

  const handleSubmit = async (e) => {
    e.preventDefault();
    setIsSubmitting(true);

    try {
      const response = await axios.post(`/OptIn/AddContact`, {
        phoneNumber: `${formData.countryCode}${formData.phoneNumber}`,
        name: formData.name,        
      });
      if (response.status === 200) {
        // Handle successful submission (e.g., show a success message)
        navigate('/TextDemo/SmsPreview', { state: { 
          phoneNumber: `${formData.countryCode}${formData.phoneNumber}` },
          contactName: formData.name
         });
      }
    } catch (error) {
      const errorMessage = error.response?.data || 'An error occurred';
      alert(errorMessage);
    } finally {
      setIsSubmitting(false);
    }
  };

  return (
    <div id="optInBody">
      <h3 className="text-center">Text Message Demo</h3>
      <div className="card">
        <div className="card-header text-center">
          Please provide your phone number below if you would like to subscribe for Text Message Surveys
        </div>
      </div>
      <br />
      <form id="contactupdate" onSubmit={handleSubmit}>
        <div className="row">
          <label className="col-sm-3">Name:</label>
          <div className="col-sm-9">
            <input
              type="text"
              id="Name"
              name="name"
              value={formData.name}
              onChange={handleInputChange}
              title="Please use only letters, hyphens, and apostrophes"
              required
              className="form-control"
            />
          </div>
        </div>
        <br />
        <br />
        <div className="row">
          <label className="col-sm-3" htmlFor="PhoneNumber">Phone Number:</label>
          <div className="col-sm-9">
            <div className="input-group">
              <select
                className="form-select country-code-select"
                id="countryCode"
                name="countryCode"
                value={formData.countryCode}
                onChange={handleInputChange}
              >
                <option value="1" selected>+1 (US)</option>
                <option value="44">+44 (UK)</option>
                <option value="52">+52 (MX)</option>
              </select>
              <input
                type="text"
                className="form-control"
                id="PhoneNumber"
                name="phoneNumber"
                inputMode="numeric"
                value={formData.phoneNumber}
                onChange={handleInputChange}
                required
              />
            </div>
          </div>
        </div>
        <div className="form-group text-center">
          <button
            type="submit"
            className="btn btn-primary"
            disabled={isSubmitting}
          >
            {isSubmitting ? 'Submitting...' : 'Submit'}
          </button>
        </div>
      </form>
      <div className="card text-center">
        <div className="card-footer text-muted">
          By pressing "Submit," you consent to receive text messages from Tom Built This at the phone number provided. These messages may include requests for feedback on your experience and other business-related communications. Message frequency may vary. Standard message and data rates may apply. Reply STOP at any time to opt out.
        </div>
      </div>
    </div>
  );
};

export default SmsOptIn; 