import React, { useState, useEffect } from 'react';
import { useLocation } from 'react-router-dom';
import axios from 'axios';

const SmsPreview = () => {
    const location = useLocation();
    const { contactName, phoneNumber } = location.state || {}; // Destructure the state

    const [isSending, setIsSending] = useState(false);
    const [receipt, setReceipt] = useState('');
    const [showTypingIndicator, setShowTypingIndicator] = useState(false);
    const [showInstructionBubble, setShowInstructionBubble] = useState(false);
    const [messageBody, setMessageBody] = useState('');

    useEffect(() => {
        // Show the instruction bubble after a delay
        const timer = setTimeout(() => {
            setShowInstructionBubble(true);
            setShowTypingIndicator(true);
        }, 2000); // Wait 2 seconds before showing the instruction bubble

        return () => clearTimeout(timer); // Cleanup on unmount
    }, []);

    const sendText = async () => {
        setIsSending(true);
        setReceipt('Sending...');
        setShowTypingIndicator(true);

        const countryCode = '1'; // Assuming US for this example
        const cleanedPhoneNumber = phoneNumber.replace(/\D/g, '');
        const fullPhoneNumber = `+${countryCode}${cleanedPhoneNumber}`;

        // Simulate typing indicator duration
        setTimeout(() => {
            setShowTypingIndicator(false);
            setMessageBody(`You should receive a text message shortly from +18333022086`);
        }, 2000); // Typing duration

        const baseUrl = '/Sms/AddSmsRecordAndTriggerSend'; // Adjust the URL as necessary
        const URL = `${baseUrl}?phoneNumber=${encodeURIComponent(fullPhoneNumber)}&nameForText=${encodeURIComponent(contactName)}`;

        try {
            const response = await axios.post(URL);
            if (response.status === 200) {
                setReceipt('Delivered');
            } else {
                setReceipt('Failed to send');
            }
        } catch (error) {
            setReceipt('Failed to send');
        } finally {
            setIsSending(false);
        }
    };

    return (
        <div className="text-message-container">
            <div className="message-header">
                <div className="header-content">
                    <img src="~/images/Logo_Dark_SVG.svg" alt="TDG Logo" className="header-logo" />
                    <div className="contact-name">Experience Connect</div>
                </div>
            </div>

            {showInstructionBubble && (
                <div className="text-bubble system-message" id="instructionBubble">
                    <div className="message-text" id="textInstructions">
                        Below is the text message that will be sent to: {phoneNumber}
                    </div>
                </div>
            )}

            {showTypingIndicator && (
                <div className="typing-indicator" id="typingIndicator">
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
            )}

            <div className="d-none text-bubble fade-in" id="messageID">
                <div className="message-text" id="messageBody">{messageBody}</div>
            </div>

            <span className="time d-none text-right" id="reciept">{receipt}</span>

            <div className="text-center">
                <button className="btn-primary btn" id="sendTextButton" onClick={sendText} disabled={isSending}>
                    {isSending ? 'Sending...' : 'Send Text Message'}
                    <i className="fas fa-sms"></i>
                </button>
            </div>
        </div>
    );
};

export default SmsPreview;