import { Navigate, Route, Routes } from "react-router-dom"
import { NavBar, Footer } from "../components"
<<<<<<< HEAD
import {CreateOrder, CreateClient, Login, Portal, HomePage, ListAll} from "../pages"
=======
import {CreateOrder, CreateClient, Login, Portal, HomePage,ListOrders } from "../pages"

>>>>>>> ced7ac662d9ca67275e9c8cfc5740a627d190c6a



export const PackageServiceRouter = () => {
  return (
    <div className="overflow-x-hidden bg-gray-100 w-screen h-screen bg-hero-pattern bg-no-repeat bg-cover" >
      <NavBar/>
      <div className="px-6 py-8">
            <div className="container flex justify-between mx-auto">
             <Routes>

                <Route path= '/login' element = {<Login/>}/>
                <Route path= '/home' element = {<HomePage/>}/>
                <Route path= '/createorder' element = {<CreateOrder/>}/>
                <Route path= '/createclient' element = {<CreateClient/>}/>
                <Route path= '/portal' element = {<Portal/>}/>
<<<<<<< HEAD
                <Route path= '/listall' element = {<ListAll/>}/>
                
=======
                <Route path= '/listorder' element = {<ListOrders/>}/>
>>>>>>> ced7ac662d9ca67275e9c8cfc5740a627d190c6a
                {/* <Route path= '/blog/post/:id' element = {<PostPage/>}/> */}
                <Route path= '/*' element = {<Navigate to = {"/home"}/> }/>
             </Routes>
            </div>
          </div>
          <Footer/>
        </div>
  )
}
