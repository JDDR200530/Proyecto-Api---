import { Navigate, Route, Routes } from "react-router-dom"
import { NavBar, Footer } from "../components"
import {CreateOrder, CreateClient, Login, Portal, HomePage,ListOrders } from "../pages"




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
                <Route path= '/listorder' element = {<ListOrders/>}/>
                {/* <Route path= '/blog/post/:id' element = {<PostPage/>}/> */}
                <Route path= '/*' element = {<Navigate to = {"/home"}/> }/>
             </Routes>
            </div>
          </div>
          <Footer/>
        </div>
  )
}
