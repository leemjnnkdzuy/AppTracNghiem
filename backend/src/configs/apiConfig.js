const express = require("express");
const userRoutes = require("../routes/userRoutes");
const contestRoutes = require("../routes/contestRoutes");

const router = express.Router();

router.use("/user", userRoutes);
router.use("/contest", contestRoutes);

module.exports = router;
