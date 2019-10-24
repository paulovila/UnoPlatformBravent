#Configure Visual Studio before debugging:
1. Right click **UnoWebApiSwagger.WebApi** > Properties
2. Click Debug
3. Uncheck **Launch browser**
4. Set the App URL to **http://localhost:20046/**
5. Right click **UnoWebApiSwagger.Wasm** > Properties
6. Click Debug
7. Set the App URL to **http://localhost:20044/**
8. Right click Solution 'UnoWebApiSwagger' > Set StartUp Projects
9. Select **Multiple startup projects**
10. Select **UnoWebApiSwagger.WebApi** Start
11. Select **UnoWebApiSwagger.Wasm** Start Without debugging
12. Make sure to set Google Chrome as your launch browser to debug
12. Press F5
