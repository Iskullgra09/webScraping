from flask import Flask, request, jsonify
from flask_cors import CORS

app = Flask(__name__)
CORS(app)
jsonData=[]

@app.route("/json", methods=['POST'])
def json():
    global jsonData
    if request.method == 'POST':
        jsonData = request.json
        return jsonify({"status": "ok"})

@app.route("/getData", methods=['GET'])
def getData():
    global jsonData
    return jsonify(jsonData)

if __name__ == "__main__":
	#cambiar ip de tu computador 'ipv4', ejemplo 192.168.1.186
    app.run(host='localhost')