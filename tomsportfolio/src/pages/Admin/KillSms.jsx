import React, { useState } from 'react';
import axios from 'axios';

const KillSms = () => {
  const [password, setPassword] = useState('');
  const [isAuthenticated, setIsAuthenticated] = useState(false);
  const [isSmsActive, setKillSwitch] = useState(false);
  const [error, setError] = useState('');

  // Simulate password check (replace with real API call in production)
  const handlePasswordSubmit = async (e) => {
    e.preventDefault();
    // Replace this with a real API call for security!
    var url = `${import.meta.env.VITE_API_URL_HTTP}/api/Test/admin/login`;
    var response = await axios.post(url, {password});
    if(response.status == 200){
      setIsAuthenticated(true);
      setKillSwitch(response.data.isSmsactive);
      setError('');
    }
    else {
      setError('Incorrect password');
    }
    if (password === 'YourSecretPassword') {
      
      // Optionally fetch current killSwitch status from backend here
    } 
  };

  const handleToggle = async () => {
    try {
      // Call your backend API to update the kill switch status
      await axios.post(`${import.meta.env.VITE_API_URL_HTTP}/api/Test/admin/toggle-sms`, { SetSmsStatus: !isSmsActive, password: password });
      setKillSwitch(!isSmsActive);
    } catch (err) {
      setError('Failed to update kill switch');
    }
  };

  return (
    <div>
      <h2>Kill SMS Admin</h2>
      {!isAuthenticated ? (
        <form onSubmit={handlePasswordSubmit}>
          <input
            type="password"
            placeholder="Enter admin password"
            value={password}
            onChange={e => setPassword(e.target.value)}
          />
          <button type="submit">Submit</button>
          {error && <div style={{ color: 'red' }}>{error}</div>}
        </form>
      ) : (
        <div>
            <div>
                <label>Sms Is Currently: {isSmsActive ? 'Active' : 'Disabled'}</label>
            </div>
          <label>
            <input
              type="button"
              value={isSmsActive ? 'Disable' : 'Enable'}
              onClick={handleToggle}
            />
          </label>
          {error && <div style={{ color: 'red' }}>{error}</div>}
        </div>
      )}
    </div>
  );
};

export default KillSms;