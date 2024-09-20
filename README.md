
## <a name="_cat9bhecbw4l"></a>**Overview**
This project was developed during my internship at Niso Software Technologies, aiming to build a career platform where employers and job seekers can connect based on mutual needs and qualifications. The core idea is to optimize the recruitment process by matching job postings with relevant candidate profiles using a custom algorithm. The platform leverages **Vue.js** for frontend, **ASP.NET Core** for backend services, and a **Python-based algorithm** for processing and matching profiles with job listings.
## <a name="_iyuqacrw3mhk"></a>**Objectives**
- Build a **full-stack web application** that allows employers to post jobs and candidates to apply by submitting their CVs.
- Develop a **custom matching algorithm** that evaluates the compatibility of job listings and CVs based on a scoring mechanism.
- Ensure seamless integration between the frontend (Vue.js), backend (ASP.NET Core), and algorithmic services (Python).
## <a name="_2lsulfjklg2k"></a>**Technologies Used**
- **Frontend:** Vue.js (Options API) for creating dynamic and interactive UI components.
- **Backend:** ASP.NET Core for managing API requests and processing data.
- **Algorithm:** Python for developing a data processing engine and matching algorithm.
- **Database:** Initial development uses JSON for data storage, with plans for future integration with PostgreSQL/MySQL.
## <a name="_oo4sqhhk5gk8"></a>**Features**
### <a name="_lypisyxrhr58"></a>**1. Job Listings Management**
Employers can create, view, and manage job listings on the platform. The interface for creating a job listing includes input fields such as job title, description, required experience, and key skills.

- **Frontend:** Vue.js manages the creation and display of job listings, using Axios to send data to the backend API.
- **Backend:** ASP.NET Core handles storing job listings, initially in a JSON file with plans for database support.

**Example:** When an employer posts a job, the Vue.js frontend communicates with the backend using an Axios POST request. The job listing is then stored, and it is visible to candidates on the listings page.

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 001](https://github.com/user-attachments/assets/725885d2-a324-4d6f-bccd-f85db67b7019)

**Figure 1: Advert Creation Scene** 

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 002](https://github.com/user-attachments/assets/871c9d58-1254-4300-9f31-a833d7fb3ad7)

**Figure 2 : Advert List Scene**
### <a name="_cnbw5tdxrgl1"></a>**2. Candidate Profile and CV Management**
Candidates can upload their CVs, detailing their skills and experience. A key feature of this system is the **skills validation** process, where candidates demonstrate their proficiency in listed skills through associated projects.

- **CV Creation:** Candidates submit their profiles.
- **Skill Validation:** The platform verifies candidate skills by associating them with specific projects rather than relying on general claims.

**Example:** A candidate listing "Python" as a skill must link to a project demonstrating their Python experience. This approach adds reliability to CVs.

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 003](https://github.com/user-attachments/assets/859b0e6b-1e1a-4a25-aff3-fbfdff3b0604)

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 004](https://github.com/user-attachments/assets/3cdcb96f-082b-494a-bfe1-10d569816aa4)

**Figure 3 : Cv Upload scene**

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 005](https://github.com/user-attachments/assets/8b0e075d-f410-4576-9b24-90e5a1e06639)

**Figure 4 : CV List scene**

### <a name="_rd41tvjj80vj"></a>**3. Matching Algorithm**
The custom algorithm designed in Python evaluates the compatibility between job listings and candidate profiles. The algorithm computes a **matching score** by comparing the required skills for a job with the candidate’s listed skills.

- **Mean Squared Error (MSE):** This method calculates the difference between required and actual skills to generate a matching score.
- **Skill Weighting:** Employers can assign a weight to each required skill (on a scale from 1 to 5), indicating its importance. A mandatory skill option is also available, which excludes candidates who lack that skill.

**Matching Workflow:**

1. **Job Posting:** An employer defines the skills needed and the importance (weight) of each.
1. **Candidate CV:** A candidate’s CV is processed to compare skills with the job posting.
1. **Score Calculation:** The matching algorithm calculates a score using MSE, with weighted parameters factored in.
1. **Results:** Only candidates with a score above a set threshold are considered a match, and employers only see CVs meeting the requirements.

**Example:** If "Python" is a required skill with high weight, and a candidate lists it, their matching score will be higher. If "SQL" is marked as mandatory, candidates without SQL won’t be considered, even if they score well in other areas.

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 006](https://github.com/user-attachments/assets/04f2b034-9590-417a-8053-0c2615e56b8d)

**Figure 5 : Matched Cvs For An Advert**

![Aspose Words 0c8f3da1-b6cb-410a-a0d3-250e850cdd4c 007](https://github.com/user-attachments/assets/5814ecbc-63df-437c-b135-bc9c59aacd8e)

**Figure 6 : Matched Adverts For A Cv**
### <a name="_506v2phjdrit"></a>**4. Frontend-Backend Communication**
The system uses **Axios** to facilitate communication between the frontend (Vue.js) and backend (ASP.NET Core) services. Data such as job postings, CVs, and updates flow seamlessly between the two layers via API calls.

- **Vue.js (Frontend):** Handles user inputs, page navigation, and UI updates.
- **ASP.NET Core (Backend):** Manages job listings, CVs, and interaction with the Python-based matching algorithm via RESTful APIs.

**Example:** When a candidate submits a new CV, Vue.js triggers an Axios call to the backend, where the CV data is stored. The Python service is then called to recalculate the matching scores.
### <a name="_nhtjv12j91po"></a>**5. Dynamic API Integration**
- **Python Service:** The Python service runs the matching algorithm based on the current data from the backend. This service is triggered whenever new CVs or job listings are added or updated.
- **RESTful API:** Communication between the Vue.js frontend, ASP.NET Core backend, and Python services is handled via RESTful APIs, ensuring real-time updates.
## <a name="_fuasf413wj9v"></a>**Architecture and Flow**
1. **Frontend (Vue.js):** Responsible for creating the user interface, handling form inputs, and displaying data such as job listings and matching results.
1. **Backend (ASP.NET Core):** Manages data storage, handles requests, and triggers the Python service for matching calculations.
1. **Python Algorithm Service:** Processes the data to calculate matching scores and returns the results to the backend.
### <a name="_hmrns5cqno6j"></a>**Project Workflow**
1. **User Interaction:**
   1. Candidates upload CVs.
   1. Employers post job listings.
1. **Backend Processing:**
   1. Data is sent to the backend for storage.
1. **Algorithm Trigger:**
   1. When a new CV or job listing is added, the backend calls the Python service.
1. **Matching Algorithm:**
   1. The algorithm processes and returns matching results, updating the UI in real-time.



