import { Link } from 'react-router-dom';

function Navbar() {
    return (
        <nav className="navbar navbar-expand-lg navbar-light bg-light fixed-top px-3">
            <div className="container-fluid">
                <Link className="navbar-brand" to="/">My Portfolio</Link>

                {/* Toggler button (hamburger) */}
                <button
                    className="navbar-toggler"
                    type="button"
                    data-bs-toggle="collapse"
                    data-bs-target="#navbarNav"
                    aria-controls="navbarNav"
                    aria-expanded="false"
                    aria-label="Toggle navigation"
                >
                    <span className="navbar-toggler-icon"></span>
                </button>

                {/* Collapsible nav links */}
                <div className="collapse navbar-collapse" id="navbarNav">
                    <ul className="navbar-nav ms-auto">
                        <li className="nav-item">
                            <Link className="nav-link" to="/resume">Resume</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/text-demo">Text Demo</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/coding-nightmares">Coding Nightmares</Link>
                        </li>
                        <li className="nav-item">
                            <Link className="nav-link" to="/hobbies">Hobbies</Link>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    );
}

export default Navbar;
