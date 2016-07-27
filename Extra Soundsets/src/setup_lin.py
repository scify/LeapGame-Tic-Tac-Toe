import sys
from cx_Freeze import setup, Executable

setup(
	name = "Soundset Manager",
	version = "1",
	executables = [Executable("manager.py")]
)
