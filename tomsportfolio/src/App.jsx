import './App.css';

import { HashRouter as Router, Routes, Route } from 'react-router-dom';
import Home from './pages/Home';
import Resume from './pages/Resume';
import TextDemo from './pages/TextDemo';
import Hobbies from './pages/Hobbies';
import CodingNightmares from './pages/CodingNightmares';
import SmsOptIn from './pages/TextDemo/SmsOptIn';
import SmsPreview from './pages/TextDemo/SmsPreview';

function App() {
  return (
      <Router>
              <Routes>
                  <Route path="/" element={<Home />} />
                  <Route path="/resume" element={<Resume />} />
                  <Route path="/text-demo" element={<TextDemo />} />
                  <Route path="/hobbies" element={<Hobbies />} />
                  <Route path="/coding-nightmares" element={<CodingNightmares />} />
                  <Route path="/text-demo/sms-opt-in" element={<SmsOptIn />} />
                  <Route path="/text-demo/sms-preview" element={<SmsPreview />} />
              </Routes>
      </Router>
  )
}

export default App