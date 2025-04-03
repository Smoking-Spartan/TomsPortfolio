import { useEffect, useState } from 'react';
import { motion, AnimatePresence } from 'framer-motion';
import {BaseLayout} from '../../components/Layout';
import { useLocation } from 'react-router-dom';

export default function TextPreview() {
  const location = useLocation();
  const { phoneNumber, contactName, justOptedIn } = location.state || {};
  const [step, setStep] = useState(0);
  const [sent, setSent] = useState(false);
  const [showAlert, setShowAlert] = useState(justOptedIn);
  const [isVisible, setIsVisible] = useState(justOptedIn);

  useEffect(() => {
    if (!contactName || !phoneNumber) return;

    const timers = [
      setTimeout(() => setStep(1), 2000), // Show instruction
      setTimeout(() => setStep(2), 3000), // Show typing
      setTimeout(() => setStep(3), 5000), // Show message
    ];

    return () => timers.forEach(clearTimeout);
  }, [contactName, phoneNumber]);
  
  useEffect(() => {
    if (justOptedIn) {
      // Start fade out after 1.5 seconds
      const fadeTimer = setTimeout(() => {
        setIsVisible(false);
      }, 1500);

      // Cleanup timers if component unmounts
      return () => {
        clearTimeout(fadeTimer);
      };
    }
  }, [justOptedIn]);

  const sendText = () => {
    setSent(true);
    setStep(4);
    // You can hook this up to your backend via fetch/AJAX here
  };

  return (
    <BaseLayout>
    <div className="relative pt-16"> 
      {showAlert && (
        <div className={`alert alert-success alert-fade ${isVisible ? 'show' : 'hide'}  absolute top-4 left-0 right-0 z-10`}
        role="alert">
          Successfully opted in! You should receive a confirmation message shortly.
        </div>
      )}
      <div className="text-message-container bg-white rounded-xl p-4 shadow-md max-w-md mx-auto">
        {/* Header */}
        <div className="header-content">
          <img 
            src={`${import.meta.env.BASE_URL}/media/TBT_Logo.png`} 
            alt="Contact" 
            className="header-logo"
          />
          <span className="contact-name">Tom Evanko</span>
        </div>

        {/* Instruction Bubble */}
        <AnimatePresence>
          {step >= 1 && (
            <motion.div
              key="instruction"
              className="text-bubble system-message bg-gray-200 rounded-xl px-4 py-2 mb-2"
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0 }}
            >
              Thanks for Opting In {contactName}!
              <br/>
              Below is the text message that will be sent to: {phoneNumber}
            </motion.div>
          )}
        </AnimatePresence>

        {/* Typing Indicator */}
        <AnimatePresence>
          {(step === 2 || step === 4) && (
            <motion.div
              key="typing"
              className="typing-indicator flex space-x-1 mb-2"
              initial={{ opacity: 0 }}
              animate={{ opacity: 1 }}
              exit={{ opacity: 0 }}
            >
              <span className="w-2 h-2 bg-gray-400 rounded-full animate-bounce"></span>
              <span className="w-2 h-2 bg-gray-400 rounded-full animate-bounce delay-100"></span>
              <span className="w-2 h-2 bg-gray-400 rounded-full animate-bounce delay-200"></span>
            </motion.div>
          )}
        </AnimatePresence>

        {/* Message Bubble */}
        <AnimatePresence>
          {step >= 3 && (
            <motion.div
              key="message"
              className="text-bubble bg-blue-500 text-white rounded-xl px-4 py-2 self-end mb-2"
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0 }}
            >
              Hey there {contactName}! Here is an example text message you would receive from Tom Built This
            </motion.div>
          )}
        </AnimatePresence>
        
        
        {/* Send Button */}
        {step >= 3 && !sent && (
          <div className="mt-4">
        <div className="ios-message-bar">
          <div className="ios-message-input">

            <span className="message-text">Hey there {contactName}! Here is an example text message you would receive from Tom Built This</span>
          </div>
          <button
            style={{ backgroundColor: '#007AFF' }}
            className="send-button"
            onClick={sendText}
            disabled={true}
          >
            Send
          </button>
        </div>
        </div>
        )}

        {/* Final Confirmation Bubble */}
        <AnimatePresence>
          {step === 4 && (
            <motion.div
              key="followup"
              className="text-bubble system-message btn-primary bg-gray-100 text-blue-800 rounded-xl px-4 py-2 mt-4"
              initial={{ opacity: 0, y: 10 }}
              animate={{ opacity: 1, y: 0 }}
              exit={{ opacity: 0 }}
            >
              You should receive a text message shortly from +18333022086
            </motion.div>
          )}
        </AnimatePresence>
      </div>
      </div>
    </BaseLayout>
  );
}