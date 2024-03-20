const functions = require("firebase-functions");
const admin = require("firebase-admin");

admin.initializeApp();

exports.createUser = functions.https.onRequest(async (req, res) => {
  try {
    // Verifique se é um pedido POST
    if (req.method !== "POST") {
      return res.status(405).json({error: "Method Not Allowed"});
    }

    // Obtenha os dados do corpo da solicitação
    const userData = req.body;
    console.log(req.body);
    // Realize as validações necessárias dos dados recebidos, se aplicável

    // Crie o usuário no Firestore ou em outro local desejado
    const user = await admin.firestore().collection("users").add(userData);

    // Responda com sucesso
    return res.status(200).json({message: "User created", userId: user.id});
  } catch (error) {
    console.error("Error creating user:", error);
    return res.status(500).json({error: "Internal Server Error"});
  }
});
