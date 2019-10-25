### Configure Visual Studio before debugging
1. Right click **UnoWebApiSwagger.WebApi** > *Properties* > *Debug* > **Uncheck** *Launch browser* and set the *App URL* to **http://localhost:20046/**
2. Right click **UnoWebApiSwagger.Wasm** > *Properties* > *Debug* > Set the *App URL* to **http://localhost:20044/**
3. Right click **Solution 'UnoWebApiSwagger'** > *Set StartUp Projects...* > *Select Multiple startup projects*
4. Select *UnoWebApiSwagger.WebApi* > *Start*
5. Select *UnoWebApiSwagger.Wasm* > *Start Without debugging*
6. Make sure to set Google Chrome as your launch browser to debug


