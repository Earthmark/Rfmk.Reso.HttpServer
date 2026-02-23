
const logContainer = document.getElementById("log-viewer");
const statusIndicator = document.getElementById("connection-status");

let socket;
let reconnectInterval = 3000;

function connect() {
    socket = new WebSocket('/api/log/ws');

    socket.onopen = () => {
        console.log("Log connection opened");
        updateStatus(true);
    }

    socket.onmessage = (event) => {
        appendLog(event.data);
    };

    socket.onclose = () => {
        console.log("Log connection closed");
        updateStatus(false);
        setTimeout(connect, reconnectInterval);
    }

    socket.onerror = (error) => {
        console.error("Log connection error:", error);
        socket.close();
    }
}

function appendLog(message) {
    const msg = JSON.parse(message);
    const messageNode = document.createElement("tr");
    const severityNode = document.createElement("td");
    const messageValueNode = document.createElement("td");
    messageNode.append(severityNode, messageValueNode);
    severityNode.textContent = msg.severity;
    messageValueNode.textContent = msg.message;
    
    logContainer.appendChild(messageNode);
}

function updateStatus(isConnected) {
    statusIndicator.classList.remove("status-connected", "status-disconnected");
    statusIndicator.classList.add(isConnected ? "status-connected" : "status-disconnected");
}

setTimeout(connect, 100);
