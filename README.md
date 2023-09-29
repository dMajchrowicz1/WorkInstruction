![logo](https://github.com/dMajchrowicz1/WorkInstruction/blob/main/ConveyorDoc/Assets/appLogo96.png) 
# Work Instruction

Full Modular WPF application with PRISM architecture approach.

## Application
Work Instruction is app to generate documentation for CNC machines. 
Based on nc code:
```bash
NC CODE

(MAZAK)
T0810
G54 G94
G92 S150 P1
G97 S10 M3 P1
M71

(DRAWING CIRCLES)
(CIRCLE)
G0 X700 
G0 Z-37 
G0 X255 
G1 X250 F2000
G4 X6 
G0 X255

-----

(MAZAK)
T0810
G54 G94
(ZERO POINT LINE)
M70
M20
G28 C0
G0 C0 
G0 Z-37 
G0 X255 
G1 X250 F2000
G1 X250 W-50.46 
G1 X255.51 W-23.02441 
G1 X339.9 W-343.615 
G1 X340 W-72.62937 
G1 X339.55 W-173.8031 
G1 X340 W-20.42 
G1 X390 W-5.58 
G1 X390 W-8.66 
```

## Documentation structure
### Each documentation contains sections:
- **Tools** (Machine tools used in nc code) 
- **Fixtures** (All fixtures needed to produce component)
- **Variables** (Revisons, Nc code name, etc.)
- **Description** (Information for operator how to setup up machine)
- **Drawings** (Setup up drawings)




