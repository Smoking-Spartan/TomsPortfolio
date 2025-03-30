import { useState, useRef, useEffect } from 'react';

function HobbyCard({ hobby }) {
    const [showMedia, setShowMedia] = useState(false);
    const {
        id,
        title,
        icon,
        description,
        mediaUrl,
        mediaGallery,
        mediaType,
        showPuzzle,
        showPizza
    } = hobby;

    return (
        <div className="card h-100 shadow-sm">
            <div className="card-body">
                <h5 className="card-title">
                    {icon} {title}
                </h5>
                <p className="card-text">{description}</p>

                <button
                    className="btn btn-sm btn-outline-primary"
                    onClick={() => setShowMedia(prev => !prev)}
                >
                    {showMedia ? 'Hide' : 'Show'} {mediaType === 'video' ? 'Video' : 'Photo'}
                </button>


                {showMedia && (
                    <div className="mt-3">
                        {showPuzzle && (
                            <div className="mt-4">
                                <h6 className="fw-bold">🧠 Daily Puzzle</h6>
                                <a
                                    href="https://www.chess.com/daily-chess-puzzle"
                                    target="_blank"
                                    rel="noopener noreferrer"
                                    className="btn btn-sm btn-outline-primary"
                                >
                                    Solve Today’s Puzzle
                                </a>
                                <br />
                            </div>
                        )}
                        {showPizza && (
                            <div className="row g-3">
                                {mediaGallery.map((img, i) => (
                                    <div className="col-6 col-md-4" key={i}>
                                        <img src={`${import.meta.env.BASE_URL}${img}`} className="img-fluid rounded shadow-sm" alt={`Pizza ${i + 1}`} />
                                    </div>
                                ))}
                            </div>
                        )}
                        {mediaType === 'video' ? (
                            <video src={`${import.meta.env.BASE_URL}${mediaUrl}`} controls className="w-100 rounded" />
                        ) : (
                            <img src={`${import.meta.env.BASE_URL}${mediaUrl}`} alt={title} className="img-fluid rounded" />
                        )}
                    </div>
                )}
            </div>
        </div>
    );
}

export default HobbyCard;