<br>
<div align="center">
    <div >
        <img height="150px" src="./images/Logo.png" alt=""/>
    </div>
    <br>
    <div>
            <h3>Survival Shooter: The Sequel</h3>
            <p><i>Extended version of Survival Shooter game from Unity tutorial</i></p>
    </div>
</div>

<h1 align="center">Survival Shooter: The Sequel Game</h1>

<img src="./images/Main.png" alt=""/>

Survival Shooter: The Sequel expands upon the original Unity tutorial with a wealth of new features. Dive into a world ravaged by Keroco, monstrous frog-like creatures. Embark on 4 challenging quests, following a captivating narrative revealed through 3 cinematic cut-scenes. Progress is tracked with a save system, ensuring your hard-earned victories aren't lost. Failing a quest or depleting your health results in a game over, but don't worry, you can review your performance through detailed gameplay statistics.

The enhanced main menu lets you fine-tune the experience. Adjust audio levels, toggle sound effects, and select difficulty to find your perfect challenge.  A loyal pet companion, purchasable with in-game currency earned from quests, fights alongside you.  Conquer a variety of enemies – from the basic Keroco to the formidable Raja Keroco and Jendral Keroco.  Multiple weapon types, including a default weapon, shotgun, and sword, offer strategic combat options.  Enhance your abilities with power-ups found within mysterious orbs scattered throughout the world. And for those who want to push the boundaries, a selection of cheats is available for a truly wild experience. With Survival Shooter: The Sequel, the adventure awaits!

## 📝 Table of Contents
- [General Information](#general-information)
- [Tech Stacks and Platforms](#tech-stacks-and-platforms)
- [Libraries](#libraries)
- [Main Features](#main-features)
- [Bonus 1 - Build to Android](#bonus-1---build-to-android)
- [Bonus 2 - Multitype Input](#bonus-2---multitype-input)
- [Bonus 3 - Skill Cutscene](#bonus-3---skill-cutscene)
- [Bonus 4 - AR Version](#bonus-4---ar-version)
- [Task Division](#task-division)
- [Contributors](#contributors)

## 📢 General Information
The construction project of the Bandung Bondowoso temple was a great success due to the Android application created by his genie. As a result, he gained immense wealth. Confused about how to spend his fortune, Bandung Bondowoso decided to assemble a PC so that he could play AAAA games. However, he felt that AAAA games did not meet his expectations. Therefore, Bandung Bondowoso asked for help from his genie to create a new game.

The genie of Bandung Bondowoso had previously tried to create a game from the [Unity tutorial](https://youtube.com/playlist?list=PL871udVFq7OF9w5RBjyp_lcyzFuViLe8x&si=gxYZvMOM8I1uHX8w) that is also available [here](https://drive.google.com/drive/folders/1kKue0ELQlIqWxppyJ0h3W-LZBtxNb2Up?usp=sharing). Therefore, the genie of Bandung Bondowoso decided to modify the game to make it more challenging.

## 👨🏻‍💻 Tech Stacks and Platforms
<div align="center">
<kbd>
<img src="./images/icons/Unity.png" height="60" />
</kbd>
<kbd>
<img src="./images/icons/Visual Studio.png" height="60" />
</kbd>
<kbd>
<img src="./images/icons/Android.png" height="60" />
</kbd>
</div>
<div align="center">
<h4>Unity | Visual Studio | Android</h4>
</div>

## 📖 Libraries
Here's list of libraries that used in this project.
<table style="width:100%">
  <col width="35%">
  <col width="65%">
  <tr>
    <th>Libraries</th>
    <th>Purposes</th>
  </tr>
  <tr>
    <td>TextMeshPro</td>
    <td>Enables advanced text rendering with better performance and visual enhancements.</td>
  </tr>
  <tr>
    <td>Timeline</td>
    <td>Creates cinematic sequences and animations with control over cameras, sound, and transitions.</td>
  </tr>
  <tr>
    <td>Unity UI</td>
    <td>Builds user interfaces within the game, including menus, buttons, and health bars.</td>
  </tr>
  <tr>
    <td>Newtonsoft Json</td>
    <td>Parses and manipulates JSON data for saving game data or working with external sources.</td>
  </tr>
  <tr>
    <td>Version Control</td>
    <td>Tracks changes to project files over time, allowing reverts and collaboration.</td>
  </tr>
  <tr>
    <td>Visual Scripting</td>
    <td>Creates game logic visually with nodes instead of writing code.</td>
  </tr>
</table>

And some **Unity core libraries** like Unity, UnityEngine, System, and many more!

## ⭐ Game Flows
Here's Survival Shooter The Sequel main game flows and its interface.
<table style="width:100%; text-align:center;">
    <col width="49%">
    <col width="49%">
    <tr>
        <td width="1%" align="center"><img src="./images/screenshots/Splash Screen.jpg"/></td>
        <td width="1%" align="center"><img src="./images/screenshots/Login Page.jpg"/></td>
    </tr>
    <tr>
        <td width="1%" align="center">Features 1</td>
        <td width="1%" align="center">Features 2</td>
    </tr>
    <tr>
        <td width="1%" align="center"><img src="./images/screenshots/Splash Screen.jpg"/></td>
        <td width="1%" align="center"><img src="./images/screenshots/Login Page.jpg"/></td>
    </tr>
    <tr>
        <td width="1%" align="center">Features 1</td>
        <td width="1%" align="center">Features 2</td>
    </tr>
</table>

## 🤳 Bonus 1 - Twibbon Feature
<table style="width:100%; text-align:center;">
    <col width="32%">
    <col width="32%">
    <col width="32%">
    <tr>
        <td width="1%" align="center"><img src="./assets/screenshots/Twibbon Page.jpg"/></td>
        <td width="1%" align="center"><img src="./assets/screenshots/Twibbon Result 1.jpg"/></td>
        <td width="1%" align="center"><img src="./assets/screenshots/Twibbon Result 2.jpg"/></td>
    </tr>
    <tr>
        <td width="1%" align="center">Twibbon Page</td>
        <td width="1%" align="center">Twibbon Result #1</td>
        <td width="1%" align="center">Twibbon Result #2</td>
    </tr>
</table>
Twibbon is what we called when we overlay a base image with another image to create a new and creative picture. With this feature, user can choose between 6 different and funny overlay to go with their image. They can capture their photo, and immediately receive a twibbon of it. They can use this feature as much as they like and experiment with the different overlay provided.

## 💥 Bonus 2 - OWASP Analysis
OWASP (Open Web Application Security Project) Top 10 is a list of the ten most critical application security risks. Here is our analysis on 3 of them and how we address those security vulnerabilities.

### 🔒 M4: Insufficient Input/Output Validation
Insufficient validation and sanitization of data from external sources, such as user inputs or network data, in a mobile application can introduce severe security vulnerabilities. Mobile apps that fail to properly validate and sanitize such data are at risk of being exploited through attacks specific to mobile environments, including SQL injection, Command Injection, and cross-site scripting (XSS) attacks.

These vulnerabilities can have detrimental consequences, including unauthorized access to sensitive data, manipulation of app functionality, and potential compromise of the entire mobile system.

Inadequate output validation can result in data corruption or presentation vulnerabilities, allowing malicious actors to inject malicious code or manipulate sensitive information displayed to users.

An application can be vulnerable to insufficient input/output validation due to:
- **Lack of Input Validation**: Failure to properly validate user input can expose the application to injection attacks like SQL injection, command injection, or XSS.
- **Inadequate Output Sanitization**: Insufficient sanitization of output data can result in XSS vulnerabilities, allowing attackers to inject and execute malicious scripts.
- **Context-Specific Validation Neglect**: Neglecting to consider specific validation requirements based on data context can create vulnerabilities, such as path traversal attacks or unauthorized access to files.
- **Insufficient Data Integrity Checks**: Not performing proper data integrity checks can lead to data corruption or unauthorized modification, compromising reliability and security.
- **Poor Secure Coding Practices**: Neglecting secure coding practices, such as using parameterized queries or escaping/encoding data, contributes to input/output validation vulnerabilities.

Here are how we address the vulnerabilities.
1. **Unsanitized Query and Improper Parameter** 
To prevent SQL Injection attacks, we use Room as persistence mechanism and define the queries and its parameters in DAO (Data Access Object) interface. Room will handle the parameter binding and ensure the execution of properly sanitized query. 
<img src="./assets/owasp/4-1.png"/>

2. **Improper Output From Calling Server API**
To handle malicious output given by server API when requesting something, we use data class to contain the output/response. This ensure proper and structured data type usage and correct data name.
<img src="./assets/owasp/4-2-1.png"/>
<img src="./assets/owasp/4-2-2.png"/>

3. **Unexpected User-Generated Output**
To handle occasion where processing user input leads to null value, we use default value so that the application still behaves expectedly when that result is used again.
<img src="./assets/owasp/4-3.png"/>

4. **Malformed User Input Data**
Ensuring that user fills all the required data before processing to prevent unexpected output that may affect subsequent usage of that output.
<img src="./assets/owasp/4-4-1.png"/>
<img src="./assets/owasp/4-4-2.png"/>

### 🔒 M8: Security Misconfiguration
Security misconfiguration in mobile apps refers to the improper configuration of security settings, permissions, and controls that can lead to vulnerabilities and unauthorized access. Threat agents who can exploit security misconfigurations are attackers aiming to gain unauthorized access to sensitive data or perform malicious actions. Threat agents can be an attacker with physical access to the device, a malicious app on the device that exploits security misconfiguration to execute unauthorized actions on the target vulnerable application context.

Security misconfigurations in mobile apps can be exploited through various attack vectors, including:
- **Insecure default settings**: Mobile apps often come with default configurations that may have weak security settings or unnecessary permissions enabled, making them vulnerable to attacks.
- **Improper access controls**: Misconfigured access controls can allow unauthorized users to access sensitive data or perform privileged actions.
- **Weak encryption or hashing**: Improperly implemented or weak encryption and hashing algorithms can be exploited to gain access to sensitive information.
- **Lack of secure communication**: Failure to use secure communication protocols, such as SSL/TLS, can expose sensitive data to eavesdropping and man-in-the-middle attacks.
- **Unprotected storage**: Storing sensitive data, such as passwords or API keys, in an insecure manner, such as plain text or weakly encrypted, can lead to unauthorized access.
- **Insecure file permissions**: Storing application files with world-readable and/or world-writable permissions.
- **Misconfigured session management**: Improper session management can result in session hijacking, allowing attackers to impersonate legitimate users.

Here are how we address the vulnerabilities.
1. **Debugging Enabled in Manifest**
To prevent exposing sensitive information in debug mode, set `android:debuggable="false"` in the application tag of the AndroidManifest.xml file.
<img src="./assets/owasp/8-1.png"/>

2. **Unprotected Exported Components**
To protect activities from unauthorized access, set `android:exported="false"` for activities that don't need to be accessed by other applications.
<img src="./assets/owasp/8-2.png"/>

3. **Improper Content Provider Permissions**
Limit access to content providers by setting appropriate permissions and ensuring `android:exported="false"` unless necessary for external access.
<img src="./assets/owasp/8-3.png"/>

4. **Improper Content Provider Permissions**
Utilize internal storage or external storage with proper permissions. Before write to user's external download directory, we ask the user permission first. If the user permissions has already been granted then proceed the file saving.
<img src="./assets/owasp/8-4.png"/>

### 🔒 M9: Insecure Data Storage
Insecure data storage in a mobile application can attract various threat agents who aim to exploit the vulnerabilities and gain unauthorised access to sensitive information. These threat agents include skilled adversaries who target mobile apps to extract valuable data, malicious insiders within the organisation or app development team who misuse their privileges, state-sponsored actors conducting cyber espionage, cybercriminals seeking financial gain through data theft or ransom, script kiddies utilising pre-built tools for simple attacks, data brokers looking to exploit insecure storage for selling personal information, competitors and industrial spies aiming to gain a competitive advantage, and activists or hacktivists with ideological motives.

Here are common manifestations of these issues:
- **Lack of Access Controls**: Insufficient access controls within the application may allow unauthorized users or attackers to gain access to sensitive data stored on the device or in the app’s databases.
- **Inadequate Encryption**: Failure to properly encrypt sensitive data can result in unintended data leakage if an attacker gains access to the storage location. Without encryption, the data is easily readable and can be exploited.
- **Unintentional Data Exposure**: Mobile applications may inadvertently expose sensitive data through application logs, error messages, or debug features, allowing unauthorized individuals to view or capture sensitive information.
- **Poor Session Management**: Weak session management can lead to unintended data leakage. If session tokens or user authentication information are not adequately protected or managed, they can be intercepted or manipulated, allowing unauthorized access to sensitive data.
- **Insufficient Input Validation**: Inadequate input validation and data sanitization can lead to unintended data leakage. Attackers may exploit this weakness to inject malicious scripts or retrieve sensitive data by manipulating input fields.
- **Cloud Storage Misconfigurations**: If the mobile application uses cloud storage services for data storage and the configurations are mismanaged or misconfigured, it can result in unintended exposure or unauthorized access to stored data.
- **Third-Party Library Vulnerabilities**: Insecure third-party libraries used in the mobile application may have vulnerabilities that could lead to unintended data leakage. Attackers can exploit these vulnerabilities to gain unauthorized access to sensitive information.
- **Unintended Data Sharing**: Improper handling of data sharing features within the application can result in unintended data leakage. If sensitive data is shared with unintended recipients or if the sharing process is not adequately secured, it can lead to privacy breaches

Here are how we address the vulnerabilities.
1. **The Appropriate Implementation of Permissions**
by utilizing the Android permission system, which allows users to grant specific permissions for access to certain data or features such as location access permission and gallery access permission.
<img src="./assets/owasp/9-1-1.png"/>
<img src="./assets/owasp/9-1-2.png"/>

2. **Strong Session Management**
by implementing robust session management to protect user data and utilizing unique session tokens and ensure they are securely stored on the device.
<img src="./assets/owasp/9-2.png"/>

3. **Careful Data Sharing Management.**
When handling data sharing features, we provided adequate control to the users.
<img src="./assets/owasp/9-3.png"/>

4. **Careful Data Log Management and Error Handling**
by ensuring that sensitive data is not included in application logs. Additionally, we implemented error handling mechanisms that prevent the logging of sensitive data in error messages.
<img src="./assets/owasp/9-4.png"/>


## 🙋‍♂️ Bonus 3 - Accessibility Testing
Accessibility Scanner is a tool developed by Google and used to scan an application's user interface to provide recommendations on how to improve the application's accessibility. Accessibility Scanner enables anyone, not just developers, to quickly and easily identify common accessibility improvements; for example, zooming in on small touch targets, increasing contrast for text and images, and providing content descriptions for unlabeled graphic elements.

The following is what the suggestion page looks like before the application is repaired.
<table style="width:100%; text-align:center;">
    <col width="49%">
    <col width="49%">
    <tr>
        <td width="1%" align="center"><img src="./assets/accessibility/initial_by_screen.jpg"/></td>
        <td width="1%" align="center"><img src="./assets/accessibility/initial_by_category.jpg"/></td>
    </tr>
    <tr>
        <td width="1%" align="center">Result by Page</td>
        <td width="1%" align="center">Result by Category</td>
    </tr>
</table>

It was found that there were **171 suggestions** from various different pages. We fulfill all these suggestions based on their categories.
1. **Item Label** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/ab716260eed6a21d52bdea41ed72232b9dcad370). Here is the updates:
    - For EditTexts or editable TextViews, use an `android:hint` attribute to indicate the purpose of the text field.
    - Use an `android:labelFor` attribute to indicate that a View should act as a content label for another View.
2. **Touch Target** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/992112745541e4300f45354d0b2469ee691984b9). Here is the updates:
    - Consider making the height of this touch clickable item target 48dp or larger.
    - A parent container may be handling touch events for this item. If selecting the larger container performs the same action as selecting this item, consider defining this item as not clickable. If a different action is performed, consider increasing the size of this item.
3. **Text Contrast** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/0515b2c1ae5fff32cdbe9fdf353881226ca8859d). Here is the updates:
    - When using `TextView` to display text, use `android:textColor` and `android:background` to define foreground and background colors with a high contrast ratio.
    - The W3C recommends:
        - At least **4.5:1** for small text (below 18 point regular or 14 point bold).
        - At least **3.0:1** for large text (18 point and above regular or 14 point and above bold).
4. **Item Descriptions** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/a8cf6fd0cdec3c9c0897f072b4be766364aa9887) and [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/759cbedfbf5614d47dcd2a8e08275a8d13082089). Here is the updates:
    - Each element should have a unique text label that describes its visual meaning, purpose, or associated action.
    - Layouts used as rows within `ListViews` or `RecyclerViews` are often inflated repeatedly. In such a case, the repeated labels should include additional text with more identifying information or context for the description of the item.
5. **Image Contrast** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/cf9c41c876592a11f7e911cf54f9a64d02cc05be). Here is the updates: <br>
    When using ImageView or Image to render graphical content or iconography, make sure that contrast between foreground and background colors meets or exceeds the recommended ratios.
6. **Unexposed Text** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/6c04db96aef6fb443e9804021949a6cc3708f669). Here is the updates:
    - Visible text in a user interface component should be available to accessibility services.
    - A ViewGroup that represents a list row is labeled with a contentDescription that customizes the ordering descendant TextViews.
7. **Item Type Label** <br>
The fix is documented on [this commit](https://gitlab.informatika.org/mikeleo03/if3210-2024-android-ymr/-/commit/6c04db96aef6fb443e9804021949a6cc3708f669). Here is the updates:
    - When TalkBack announces a clickable Button element, it appends the word "button" to the text label provided by the developer. If the developer includes "button" in the text label (such as "Save button"), TalkBack might speak the word "button" twice ("Save button button").
    - A well-implemented user interface doesn't add element type or state descriptions to values of `android:contentDescription`, `android:text`, or `android:hint` attributes.

After fulfilling all these quite tiring suggestions, BondoMan finally succeeded in achieving high accessibility with no further recommendations. Here is the final result.

<table style="width:100%; text-align:center;">
    <col width="49%">
    <col width="49%">
    <tr>
        <td width="1%" align="center"><img src="./assets/accessibility/final_by_screen.jpg"/></td>
        <td width="1%" align="center"><img src="./assets/accessibility/final_by_category.jpg"/></td>
    </tr>
    <tr>
        <td width="1%" align="center">Result by Page</td>
        <td width="1%" align="center">Result by Category</td>
    </tr>
</table>

## 👨‍👨‍👦‍👦 Task Division
The following is the division of tasks that we carry out.
<table style="width:100%">
    <col width="70%">
    <col width="10%">
    <col width="10%">
    <col width="10%">
    <tr>
        <th>Task</th>
        <th>13521062</th>
        <th>13521063</th>
        <th>13521108</th>
    </tr>
    <tr id="merged-row">
        <td colspan="4"><b>Initialization</b></td>
    </tr>
    <tr>
        <td>Initiate Project</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Setup Room Database</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr id="merged-row">
        <td colspan="4"><b>Main Features</b></td>
    </tr>
    <tr>
        <td>Header dan Navbar</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Login - Halaman Login</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Logout - Halaman Pengaturan</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Melakukan Penambahan, Pengubahan, dan Penghapusan Transaksi</td>
        <td></td>
        <td align="center">✅</td>
        <td></td>
    </tr>
    <tr>
        <td>Melihat Daftar Transaksi yang Sudah Dilakukan</td>
        <td></td>
        <td align="center">✅</td>
        <td></td>
    </tr>
    <tr>
        <td>Melakukan Scan Nota - Halaman Scan Nota</td>
        <td align="center">✅</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Melihat Graf Rangkuman Transaksi - Halaman Graf</td>
        <td align="center">✅</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Menyimpan Daftar Transaksi dalam Format .xlsx, .xls - Halaman Pengaturan</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Intent GMail - Halaman Pengaturan</td>
        <td></td>
        <td align="center">✅</td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Background Service - Mengecek expiry JWT</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Network Sensing - Deteksi Sinyal</td>
        <td align="center">✅</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>Broadcast Receiver - Randomize Transaksi dari Pengaturan</td>
        <td align="center">✅</td>
        <td align="center">✅</td>
        <td></td>
    </tr>
    <tr id="merged-row">
        <td colspan="4"><b>Bonus</b></td>
    </tr>
    <tr>
        <td>Twibbon</td>
        <td align="center">✅</td>
        <td></td>
        <td></td>
    </tr>
    <tr>
        <td>OWASP</td>
        <td align="center">✅</td>
        <td align="center">✅</td>
        <td align="center">✅</td>
    </tr>
    <tr>
        <td>Accessibility Testing</td>
        <td></td>
        <td></td>
        <td align="center">✅</td>
    </tr>
</table>

## 👏 Contributors
|Contributors|Preparation Time|Working Time|
| --- | :---: | :---: |
| 13521052 - Melvin Kent Jonathan | - hours  | - hours |
| 13521056 - Daniel Egiant Sitanggang | - hours  | - hours |
| 13521064 - Bill Clinton | - hours | - hours |
| 13521108 - Michael Leon Putra Widhi | 20 hours | 48 hours |
| 13521172 - Nathan Tenka | - hours | - hours |
