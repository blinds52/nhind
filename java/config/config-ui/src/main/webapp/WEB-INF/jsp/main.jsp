<%@ page language="java" contentType="text/html; charset=ISO-8859-1"
         pageEncoding="ISO-8859-1"%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
    <head>
        <%@ include file="/WEB-INF/jsp/include.jsp"%>
        <META ?http-equiv="Content-Type" ?content="text/html;charset=UTF-8">
            <title><fmt:message key="welcome.title" /></title>
    </head>

    <body>

        <%@ include file="/WEB-INF/jsp/header.jsp" %>
        <h2>Manage Domains <a href="/config-ui/config/main/search?domainName=&submitType=newdomain" style="font-size:12px;font-weight:normal;">Create New Domain</a></h2>
	
<div style="margin:5px 0;">
        
</div>			

        <div>


		
			

        <div id="side-form">

            <h3>Filter Domains</h3>
        
            <spring:url value="/config/main/search" var="formUrl"/>
            <form:form id="searchDomainForm" action="${fn:escapeXml(formUrl)}" cssClass="cleanform" commandName="searchDomainForm" method="GET">

                <div>
                <form:label path="domainName">Domain Name:
                <form:errors path="domainName" cssClass="error" />
		</form:label>
                <br/>   
                <form:input path="domainName"  />
                </div>

                <div>
                <label>Status:</label>
                <br/>   
                <form:select path="status">
                    <form:option value="" label="All"/>
                    <form:options path="status" items="${statusList}"/>
                </form:select>
                </div>

                <div>
                <button name="submitType" id="submitType" type="submit" value="search">Search</button>
                </div>
                    	<!--<form:radiobuttons path="status" items="${statusList}"/>-->
                

            </form:form>

            </div>

            

            <div id="bundle-table-col">

                <h3>Search Results</h3>

                <c:if test="${empty searchResults}">
                    <c:if test="${not empty searchTerm}">
                    Your search for "${searchTerm}" turned up no results.
                    </c:if> 
                    
                    <c:if test="${empty searchTerm}">
                    This HISP has no configured domains.
                    </c:if>
                </c:if>

                

			 <c:if test="${not empty searchResults}">

                            



		            <div id="dynamic">
		                <spring:url value="/config/domain/remove" var="formUrlremove"/>
		                <form:form name="removeForm" modelAttribute="simpleForm" action="${fn:escapeXml(formUrlremove)}" method="POST" >
		                    <table class="data" id="domain-table" style="width:auto;">
		                        <thead>
		                            <tr>
		                                <th width=20></th>
						<th>Name</th>
		                                <th>Postmaster</th>
		                                <th>Status</th>
		                                <th>Created</th>
		                                <th>Updated</th>
		                                
		                            </tr>
		                        </thead>
		                        <tbody>
		                            <!--  Put the data from the searchResults attribute here -->
		                            <c:forEach var="domain" items="${searchResults}" varStatus="rowCounter">
		                                <tr>
		                                    <spring:url value="/config/domain?id=${domain.id}" var="formUrlclick"/>
											<td><form:checkbox path="remove" value="${domain.id}" /></td>
		                                    
		<td><a href='${fn:escapeXml(formUrlclick)}'>${domain.domainName}</a></td>
		                                    <td>${domain.postMasterEmail}</td>
		                                    <td>${domain.status}</td>
		                                    <td><fmt:formatDate value="${domain.createTime.time}" pattern="MM/dd/yyyy, hh:mm"/></td>
		                                    <td><fmt:formatDate value="${domain.updateTime.time}" pattern="MM/dd/yyyy, hh:mm"/></td>
		                                    
		                                </tr>
		                            </c:forEach>
		                        </tbody>
		                        
		                    </table>
		                    <button name="submitType" id="submitType" type="submit" value="delete">Delete</button>
                                    
                                    
		                </form:form>
		            </div>
		        </c:if>




                </div>

<br clear="both"/>
        </div>
       
        <%@ include file="/WEB-INF/jsp/footer.jsp"%>