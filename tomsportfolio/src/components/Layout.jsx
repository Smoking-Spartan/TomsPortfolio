function Layout({ children }) {
    return (
        <div className="containter py-4">
            {children}
            <p>Tom's footer test</p>
        </div>
    );
}

export default Layout;