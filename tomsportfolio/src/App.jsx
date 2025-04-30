import './App.css';

import { HashRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Resume from './pages/Resume';
import TextDemo from './pages/TextDemo';
import Hobbies from './pages/Hobbies';
import CodingNightmares from './pages/CodingNightmares';
import OptIn from './pages/TextDemo/OptIn';
import SmsPreview from './pages/TextDemo/SmsPreview';
import Survey from './pages/TextDemo/Survey';
import TermsAndConditions from './pages/Legal/TermsAndConditions';
import PrivacyPolicy from './pages/Legal/PrivacyPolicy';
import TestPage from './pages/TestPage';
function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Home />} />
        <Route path="/resume" element={<Resume />} />
        <Route path="/text-demo" element={<TextDemo />} />
        <Route path="/hobbies" element={<Hobbies />} />
        <Route path="/coding-nightmares" element={<CodingNightmares />} />
        <Route path="/text-demo/opt-in" element={<OptIn />} />
        <Route path="/text-demo/sms-preview" element={<SmsPreview />} />
        <Route path="/text-demo/survey" element={<Survey />} />
        <Route path="/legal/terms-and-conditions" element={<TermsAndConditions />} />
        <Route path="/legal/privacy-policy" element={<PrivacyPolicy />} />
        <Route path="/test-page" element={<TestPage />} />
      </Routes>
    </Router>
  )
}

export default App