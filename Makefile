CandumpHT: Main.cs
	mcs $< -out:$@ -r System.Drawing

clean:
	-@rm CandumpHT
